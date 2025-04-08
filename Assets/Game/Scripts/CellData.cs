public class CellData
{
    public readonly Cell ForwardCell;
    public readonly Cell BackCell;
    public readonly Cell RightCell;
    public readonly Cell LeftCell;

    public CellData(Cell forwardCell, Cell backCell, Cell rightCell, Cell leftCell)
    {
        ForwardCell = forwardCell;
        BackCell = backCell;
        RightCell = rightCell;
        LeftCell = leftCell;
    }
}