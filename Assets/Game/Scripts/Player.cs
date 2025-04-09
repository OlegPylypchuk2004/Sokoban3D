using CellResidentMove;

public class Player : CellResident
{
    protected override IMoveType CreateMoveType()
    {
        return new JumpMoveType();
    }
}