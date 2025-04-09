using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Field : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private Cell _startCell;
    [SerializeField] private Cell[] _startBoxesCells;
    [SerializeField] private Cell[] _targetBoxesCells;

    private void OnDrawGizmos()
    {
        if (_startCell != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_startCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
        }

        if (_startBoxesCells != null)
        {
            foreach (Cell boxesCell in _startBoxesCells)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(boxesCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
            }
        }

        if (_targetBoxesCells != null)
        {
            foreach (Cell boxesCell in _targetBoxesCells)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(boxesCell.transform.position + Vector3.up * 0.25f / 2, Vector3.one * 0.25f);
            }
        }
    }

    public void Init(Player player)
    {
        InitCells();

        BoxFactory boxFactory = new BoxFactory();
        Box boxPrefab = Resources.Load<Box>("Prefabs/Box");

        foreach (Cell cell in _startBoxesCells)
        {
            Box box = boxFactory.Spawn(boxPrefab);
            box.transform.SetParent(transform);
            box.Init(cell);
        }

        player.Init(_startCell);
    }

    public bool IsAllBoxPlacesAreTaken()
    {
        foreach (Cell boxCell in _targetBoxesCells)
        {
            if (boxCell.IsEmpty() || boxCell.Resident is Box == false)
            {
                return false;
            }
        }

        return true;
    }

    private void InitCells()
    {
        ICellNeighborFinder cellNeighborFinder = new CellNeighborFinder();
        List<BoxCollider> boxColliders = new List<BoxCollider>();

        foreach (Cell cell in _cells)
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