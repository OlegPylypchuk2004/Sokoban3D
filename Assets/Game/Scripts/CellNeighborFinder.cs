using UnityEngine;

public class CellNeighborFinder : ICellNeighborFinder
{
    private const float RayOffset = 0f;
    private const float RayDistance = 0.25f;

    public Cell FindNeighbor(Cell cell, Vector3 direction)
    {
        Vector3 halfSize = cell.GetComponent<BoxCollider>().size / 2f;
        Vector3 offset = Vector3.Scale(direction, new Vector3(halfSize.x, 0, halfSize.y)) + Vector3.up / 2f;
        Vector3 origin = cell.transform.position + offset + direction.normalized * RayOffset;
        LayerMask layerMask = 1 << cell.gameObject.layer;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, RayDistance, layerMask))
        {
            if (hit.collider.TryGetComponent(out Cell neighborCell))
            {
                return neighborCell;
            }
        }

        return null;
    }
}