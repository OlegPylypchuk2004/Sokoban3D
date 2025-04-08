using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Cell _currentCell;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3.left);
        }
    }

    private void Move(Vector3 direction)
    {
        Cell nextCell = null;

        if (direction == Vector3.forward)
        {
            nextCell = _currentCell.Data.ForwardCell;
        }
        else if (direction == Vector3.back)
        {
            nextCell = _currentCell.Data.BackCell;
        }
        else if (direction == Vector3.right)
        {
            nextCell = _currentCell.Data.RightCell;
        }
        else if (direction == Vector3.left)
        {
            nextCell = _currentCell.Data.LeftCell;
        }

        if (nextCell == null || !nextCell.IsEmpty)
        {
            return;
        }

        _currentCell.IsEmpty = true;
        _currentCell = nextCell;
        _currentCell.IsEmpty = false;

        transform.position = _currentCell.transform.position + Vector3.up;
    }
}