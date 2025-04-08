using DG.Tweening;
using UnityEngine;

public abstract class CellResident : MonoBehaviour
{
    [SerializeField] protected Cell _currentCell;
    [SerializeField] private float _moveDuration;

    protected bool _isMoving;

    private void Start()
    {
        _currentCell.Resident = this;
    }

    public virtual void Move(Direction direction)
    {
        Cell targetCell = _currentCell.GetNextCell(direction);

        if (targetCell == null || !targetCell.IsEmpty())
        {
            return;
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
    }
}