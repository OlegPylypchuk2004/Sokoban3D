using UnityEngine;

public class Player : CellResident
{
    private void Update()
    {
        if (_isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Direction.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Direction.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Direction.Left);
        }
    }

    public override void Move(Direction direction)
    {
        Cell targetCell = _currentCell.GetNextCell(direction);

        if (targetCell != null && !targetCell.IsEmpty())
        {
            CellResident targetCellResident = targetCell.Resident;
            targetCellResident.Move(direction);
        }

        base.Move(direction);
    }
}