using UnityEngine;
using ReturnMoveSystem;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameplayUI _ui;

    private ISceneLoader _sceneLoader;
    private IInputHandler _inputHandler;
    private ReturnMoveManager _returnMoveManager;
    private Field _field;
    private Player _player;

    private void Start()
    {
        _sceneLoader = new CoroutineSceneLoader(this);

        PlayerFactory playerFactory = new PlayerFactory();
        Player playerPrefab = Resources.Load<Player>("Prefabs/Player");

        _player = playerFactory.Spawn(playerPrefab);
        _player.Moved += OnPlayerMoved;

        SpawnLevel();

        _inputHandler = new KeyboardInputHandler();
        _inputHandler.Received += OnInputReceived;

        _returnMoveManager = new ReturnMoveManager(10);

        _ui.Init(_player, _returnMoveManager);
    }

    private void OnDestroy()
    {
        _player.Moved -= OnPlayerMoved;
        _inputHandler.Received -= OnInputReceived;
    }

    private void Update()
    {
        if (_player.IsMoving)
        {
            return;
        }

        _inputHandler.Check();
    }

    private void SpawnLevel()
    {
        _field = Instantiate(SessionData.Level.FieldPrefab);
        _field.Init(_player, new FieldAppearAnimator());
    }

    private void OnPlayerMoved(Direction direction)
    {
        LevelCompleteCheck();
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
    }

    private void LevelCompleteCheck()
    {
        if (_field.IsAllBoxPlacesAreTaken())
        {
            _returnMoveManager.ClearMoves();
            Destroy(_field.gameObject);
            SpawnLevel();
        }
    }
}