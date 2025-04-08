using UnityEngine;

public class CellNeighborFinder : ICellNeighborFinder
{
    private const float RayOffset = 0f;
    private const float RayDistance = 0.25f;

    public Cell FindNeighbor(Cell cell, Direction direction)
    {
        Vector3 rayDirection = Vector3.zero;

        switch (direction)
        {
            case Direction.Forward:
                rayDirection = Vector3.forward;
                break;

            case Direction.Backward:
                rayDirection = Vector3.back;
                break;

            case Direction.Right:
                rayDirection = Vector3.right;
                break;

            case Direction.Left:
                rayDirection = Vector3.left;
                break;
        }

        Vector3 halfSize = cell.GetComponent<BoxCollider>().size / 2f;
        Vector3 offset = Vector3.Scale(rayDirection, new Vector3(halfSize.x, 0, halfSize.y)) + Vector3.up / 2f;
        Vector3 origin = cell.transform.position + offset + rayDirection.normalized * RayOffset;
        LayerMask layerMask = 1 << cell.gameObject.layer;

        if (Physics.Raycast(origin, rayDirection, out RaycastHit hit, RayDistance, layerMask))
        {
            if (hit.collider.TryGetComponent(out Cell neighborCell))
            {
                return neighborCell;
            }
        }

        return null;
    }
}