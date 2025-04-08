using UnityEngine;

public class CellNeighborFinder
{
    private readonly Transform _transform;
    private readonly BoxCollider _collider;
    private readonly LayerMask _cellsLayerMask;

    private const float RayOffset = 0f;
    private const float RayDistance = 0.25f;

    public CellNeighborFinder(Transform transform, BoxCollider collider, LayerMask cellsLayerMask)
    {
        _transform = transform;
        _collider = collider;
        _cellsLayerMask = cellsLayerMask;
    }

    public Cell FindNeighbor(Vector3 direction)
    {
        Vector3 halfSize = _collider.size * 0.5f;
        Vector3 offset = Vector3.Scale(direction, new Vector3(halfSize.x, 0, halfSize.y)) + Vector3.up * 0.5f;
        Vector3 origin = _transform.position + offset + direction.normalized * RayOffset;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, RayDistance, _cellsLayerMask))
        {
            if (hit.collider.TryGetComponent(out Cell neighborCell))
            {
                return neighborCell;
            }
        }

        return null;
    }
}