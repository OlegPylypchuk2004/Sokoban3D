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
            Cell forwardNeighbor = cellNeighborFinder.FindNeighbor(cell, Vector3.forward);
            Cell backNeighbor = cellNeighborFinder.FindNeighbor(cell, Vector3.back);
            Cell rightNeighbor = cellNeighborFinder.FindNeighbor(cell, Vector3.right);
            Cell leftNeighbor = cellNeighborFinder.FindNeighbor(cell, Vector3.left);

            CellData cellData = new CellData(forwardNeighbor, backNeighbor, rightNeighbor, leftNeighbor);
            cell.Init(cellData);
        }
    }
}