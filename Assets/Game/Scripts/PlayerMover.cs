using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour, ICellResident
{
    [SerializeField] private Cell _currentCell;
    [SerializeField] private float _moveDuration;

    private bool _isMoving;

    private void Update()
    {
        if (_isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(FindNextCell(Direction.Forward));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(FindNextCell(Direction.Backward));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(FindNextCell(Direction.Right));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(FindNextCell(Direction.Left));
        }
    }

    private void Move(Cell cell)
    {
        if (cell == null || !cell.IsEmpty())
        {
            return;
        }

        _currentCell.Resident = null;
        _currentCell = cell;
        _currentCell.Resident = this;

        Vector3 targetPosition = _currentCell.transform.position + Vector3.up;

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

    private Cell FindNextCell(Direction direction)
    {
        if (direction == Direction.Forward)
        {
            return _currentCell.Data.ForwardCell;
        }
        else if (direction == Direction.Backward)
        {
            return _currentCell.Data.BackCell;
        }
        else if (direction == Direction.Right)
        {
            return _currentCell.Data.RightCell;
        }
        else if (direction == Direction.Left)
        {
            return _currentCell.Data.LeftCell;
        }

        return null;
    }
}