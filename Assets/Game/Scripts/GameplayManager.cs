using UnityEngine;
using ReturnMoveSystem;

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

        _returnMoveManager = new ReturnMoveManager(_player, _cellResidents);
    }

    private void OnDestroy()
    {
        _inputHandler.Received -= OnInputReceived;
    }

    private void Update()
    {
        _inputHandler.Check();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _returnMoveManager.Return();
        }
    }

    private void OnInputReceived(Direction direction)
    {
        if (!_player.TryMove(direction))
        {
            Cell targetCell = _player.CurrentCell.GetNextCell(direction);

            if (targetCell != null && !targetCell.IsEmpty())
            {
                CellResident targetCellResident = targetCell.Resident;

                if (targetCellResident.TryMove(direction))
                {
                    _player.TryMove(direction);
                }
            }
        }
    }
}