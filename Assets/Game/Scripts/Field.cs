using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;

    private void Start()
    {
        InitCells();
    }

    private void InitCells()
    {
        ICellNeighborFinder cellNeighborFinder = new CellNeighborFinder();

        foreach (Cell cell in _cells)
        {
            Cell forwardNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Forward);
            Cell backNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Backward);
            Cell rightNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Right);
            Cell leftNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Left);

            CellData cellData = new CellData(forwardNeighbor, backNeighbor, rightNeighbor, leftNeighbor);
            cell.Init(cellData);
        }
    }
}