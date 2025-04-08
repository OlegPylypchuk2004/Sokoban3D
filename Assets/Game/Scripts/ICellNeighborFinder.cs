using UnityEngine;

public interface ICellNeighborFinder
{
    public Cell FindNeighbor(Cell cell, Direction direction);
}