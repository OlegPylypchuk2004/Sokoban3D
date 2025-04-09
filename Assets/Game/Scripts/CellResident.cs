using CellResidentMove;
using DG.Tweening;
using System;
using UnityEngine;

public abstract class CellResident : MonoBehaviour
{
    [SerializeField] protected Cell _currentCell;
    [SerializeField] private float _moveDuration;

    protected bool _isMoving;
    protected IMoveType _moveType;

    public event Action<Direction> Moved;

    private void Awake()
    {
        _moveType = CreateMoveType();
    }

    private void Start()
    {
        _currentCell.Resident = this;
    }

    public Cell CurrentCell => _currentCell;
    public bool IsMoving => _isMoving;

    public bool TryMove(Direction direction, MoveType moveType = MoveType.Simple)
    {
        if (_isMoving)
        {
            return false;
        }

        Cell targetCell = _currentCell.GetNextCell(direction);

        if (targetCell == null || !targetCell.IsEmpty())
        {
            return false;
        }

        _currentCell.Resident = null;
        _currentCell = targetCell;
        _currentCell.Resident = this;

        _isMoving = true;

        _moveType.Move(transform, _currentCell.transform.position, _moveDuration)
            .OnKill(() =>
            {
                _isMoving = false;
            });

        if (moveType == MoveType.Simple)
        {
            Moved?.Invoke(direction);
        }

        return true;
    }

    protected abstract IMoveType CreateMoveType();
}