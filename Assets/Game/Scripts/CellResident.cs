using DG.Tweening;
using System;
using UnityEngine;

public abstract class CellResident : MonoBehaviour
{
    [SerializeField] protected Cell _currentCell;
    [SerializeField] private float _moveDuration;

    protected bool _isMoving;

    public event Action<Direction> Moved;

    private void Start()
    {
        _currentCell.Resident = this;
    }

    public Cell CurrentCell => _currentCell;
    public bool IsMoving => _isMoving;

    public virtual bool TryMove(Direction direction, MoveType moveType = MoveType.Simple)
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

        Vector3 targetPosition = _currentCell.transform.position;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            _isMoving = true;
        });

        sequence.Append(transform.DOMove(targetPosition, _moveDuration)
            .SetEase(Ease.OutQuad));

        sequence.OnKill(() =>
        {
            _isMoving = false;
        });

        sequence.SetLink(gameObject);

        if (moveType == MoveType.Simple)
        {
            Moved?.Invoke(direction);
        }

        return true;
    }
}