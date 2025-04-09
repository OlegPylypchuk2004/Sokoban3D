using UnityEngine;
using ReturnMoveSystem;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CellResident[] _cellResidents;

    private IInputHandler _inputHandler;
    private ReturnMoveManager _returnMoveManager;

    private void Start()
    {
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
}