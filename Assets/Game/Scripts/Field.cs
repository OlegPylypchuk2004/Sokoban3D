using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private Box[] _boxes;
    [SerializeField] private Cell _startCell;

    private void Start()
    {
        InitCells();
    }

    private void OnDrawGizmos()
    {
        if (_startCell != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_startCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
        }
    }

    public Cell StartCell => _startCell;

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