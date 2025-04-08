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
            Move(FindNextCell(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(FindNextCell(Vector3.back));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(FindNextCell(Vector3.right));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(FindNextCell(Vector3.left));
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

    private Cell FindNextCell(Vector3 direction)
    {
        if (direction == Vector3.forward)
        {
            return _currentCell.Data.ForwardCell;
        }
        else if (direction == Vector3.back)
        {
            return _currentCell.Data.BackCell;
        }
        else if (direction == Vector3.right)
        {
            return _currentCell.Data.RightCell;
        }
        else if (direction == Vector3.left)
        {
            return _currentCell.Data.LeftCell;
        }

        return null;
    }
}