public class Player : CellResident
{
    public override void Move(Direction direction, MoveType moveType = MoveType.Simple)
    {
        Cell targetCell = _currentCell.GetNextCell(direction);

        if (targetCell != null && !targetCell.IsEmpty())
        {
            CellResident targetCellResident = targetCell.Resident;
            targetCellResident.Move(direction, moveType);
        }

        base.Move(direction, moveType);
    }
}