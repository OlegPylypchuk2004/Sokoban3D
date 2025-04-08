using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    public CellData Data { get; private set; }
    public CellResident Resident { get; set; }

    public void Init(CellData data)
    {
        Data = data;
    }

    public bool IsEmpty()
    {
        return Resident == null;
    }

    public Cell GetNextCell(Direction direction)
    {
        if (direction == Direction.Forward)
        {
            return Data.ForwardCell;
        }
        else if (direction == Direction.Backward)
        {
            return Data.BackCell;
        }
        else if (direction == Direction.Right)
        {
            return Data.RightCell;
        }
        else if (direction == Direction.Left)
        {
            return Data.LeftCell;
        }

        return null;
    }
}