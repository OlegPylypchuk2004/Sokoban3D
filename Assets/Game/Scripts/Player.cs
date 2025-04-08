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
}