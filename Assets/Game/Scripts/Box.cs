using CellResidentMove;

public class Box : CellResident
{
    protected override IMoveType CreateMoveType()
    {
        return new SlideMoveType();
    }
}