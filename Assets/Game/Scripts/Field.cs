using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private Cell _playerCell;
    [SerializeField] private Cell[] _boxesCells;

    private void Start()
    {
        InitCells();
    }

    private void OnDrawGizmos()
    {
        if (_playerCell != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_playerCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
        }

        if (_boxesCells != null)
        {
            foreach (Cell boxesCell in _boxesCells)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(boxesCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
            }
        }
    }

    public Cell PlayerCell => _playerCell;
    public Cell[] BoxesCells => _boxesCells;

    private void InitCells()
    {
        ICellNeighborFinder cellNeighborFinder = new CellNeighborFinder();
        List<BoxCollider> boxColliders = new List<BoxCollider>();

        foreach (Cell cell  in _cells)
        {
            BoxCollider boxCollider = cell.AddComponent<BoxCollider>();
            boxCollider.center = Vector3.up / 2f;
            boxCollider.size = Vector3.one * 0.75f;

            boxColliders.Add(boxCollider);
        }

        foreach (Cell cell in _cells)
        {
            Cell forwardNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Forward);
            Cell backNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Backward);
            Cell rightNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Right);
            Cell leftNeighbor = cellNeighborFinder.FindNeighbor(cell, Direction.Left);

            CellData cellData = new CellData(forwardNeighbor, backNeighbor, rightNeighbor, leftNeighbor);
            cell.Init(cellData);
        }

        for (int i = 0; i < boxColliders.Count; i++)
        {
            Destroy(boxColliders[i]);
        }
    }
}