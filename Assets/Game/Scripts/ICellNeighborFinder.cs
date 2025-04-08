using UnityEngine;

public interface ICellNeighborFinder
{
    public Cell FindNeighbor(Cell cell, Vector3 direction);
}