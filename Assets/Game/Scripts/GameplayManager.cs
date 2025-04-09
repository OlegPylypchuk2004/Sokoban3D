using UnityEngine;
using ReturnMoveSystem;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    private IInputHandler _inputHandler;
    private ReturnMoveManager _returnMoveManager;
    private Field _field;
    private Player _player;

    private void Start()
    {
        SpawnLevel();

        _inputHandler = new KeyboardInputHandler();
        _inputHandler.Received += OnInputReceived;

        _returnMoveManager = new ReturnMoveManager(10);
    }

    private void OnDestroy()
    {
        _inputHandler.Received -= OnInputReceived;
    }

    private void Update()
    {
        if (_player.IsMoving)
        {
            return;
        }

        _inputHandler.Check();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _returnMoveManager.Return();
        }

        Debug.Log(_returnMoveManager.MovesCount);
    }

    private void SpawnLevel()
    {
        _field = Instantiate(SessionData.Level.FieldPrefab);

        PlayerFactory playerFactory = new PlayerFactory();
        Player playerPrefab = Resources.Load<Player>("Prefabs/Player");

        _player = playerFactory.Spawn(playerPrefab);
        _player.transform.SetParent(_field.transform);
        _player.Init(_field.PlayerCell);

        BoxFactory boxFactory = new BoxFactory();
        Box boxPrefab = Resources.Load<Box>("Prefabs/Box");

        foreach (Cell cell in _field.StartBoxesCells)
        {
            Box box = boxFactory.Spawn(boxPrefab);
            box.transform.SetParent(_field.transform);
            box.Init(cell);
        }
    }

    private void OnInputReceived(Direction direction)
    {
        Dictionary<CellResident, Direction> moveData = new Dictionary<CellResident, Direction>();

        if (_player.TryMove(direction))
        {
            moveData.Add(_player, direction);
        }
        else
        {
            Cell targetCell = _player.CurrentCell.GetNextCell(direction);

            if (targetCell != null && !targetCell.IsEmpty())
            {
                CellResident targetCellResident = targetCell.Resident;

                if (targetCellResident.TryMove(direction))
                {
                    if (_player.TryMove(direction))
                    {
                        moveData.Add(_player, direction);
                    }

                    moveData.Add(targetCellResident, direction);
                }
            }
        }

        if (moveData.Count > 0)
        {
            _returnMoveManager.AddMoveData(moveData);
        }

        LevelCompleteCheck();
    }

    private void LevelCompleteCheck()
    {
        if (_field.IsAllBoxPlacesAreTaken())
        {
            Destroy(_field.gameObject);
            SpawnLevel();
        }
    }
}