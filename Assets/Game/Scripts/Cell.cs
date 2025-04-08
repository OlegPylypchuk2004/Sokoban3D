using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _rayOffset;
    [SerializeField] private float _rayDistance;

    private LayerMask _cellsLayerMask;

    public Cell ForwardCell { get; private set; }
    public Cell BackCell { get; private set; }
    public Cell RightCell { get; private set; }
    public Cell LeftCell { get; private set; }

    private void Awake()
    {
        FindNeighboringCells();
    }

    private void FindNeighboringCells()
    {
        _boxCollider ??= GetComponent<BoxCollider>();

        ForwardCell = null;
        BackCell = null;
        RightCell = null;
        LeftCell = null;

        _cellsLayerMask = 1 << gameObject.layer;

        Vector3 position = transform.position;
        Vector3 halfSize = _boxCollider.size * 0.5f;

        Vector3 forwardOrigin = position + Vector3.forward * (halfSize.y + _rayOffset) + Vector3.up * 0.5f;
        Vector3 backOrigin = position + Vector3.back * (halfSize.y + _rayOffset) + Vector3.up * 0.5f;
        Vector3 rightOrigin = position + Vector3.right * (halfSize.x + _rayOffset) + Vector3.up * 0.5f;
        Vector3 leftOrigin = position + Vector3.left * (halfSize.x + _rayOffset) + Vector3.up * 0.5f;

        if (Physics.Raycast(forwardOrigin, Vector3.forward, out RaycastHit hitForward, _rayDistance, _cellsLayerMask))
        {
            if (hitForward.collider.TryGetComponent(out Cell forwardCell))
            {
                ForwardCell = forwardCell;
            }
        }

        if (Physics.Raycast(backOrigin, Vector3.back, out RaycastHit hitBack, _rayDistance, _cellsLayerMask))
        {
            if (hitBack.collider.TryGetComponent(out Cell backCell))
            {
                BackCell = backCell;
            }
        }

        if (Physics.Raycast(rightOrigin, Vector3.right, out RaycastHit hitRight, _rayDistance, _cellsLayerMask))
        {
            if (hitRight.collider.TryGetComponent(out Cell rightCell))
            {
                RightCell = rightCell;
            }
        }

        if (Physics.Raycast(leftOrigin, Vector3.left, out RaycastHit hitLeft, _rayDistance, _cellsLayerMask))
        {
            if (hitLeft.collider.TryGetComponent(out Cell leftCell))
            {
                LeftCell = leftCell;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_boxCollider == null)
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        Gizmos.color = Color.red;

        Vector3 position = transform.position;
        Vector3 halfSize = _boxCollider.size * 0.5f;

        Vector3 forwardOrigin = position + Vector3.forward * (halfSize.y + _rayOffset) + Vector3.up * 0.5f;
        Vector3 backOrigin = position + Vector3.back * (halfSize.y + _rayOffset) + Vector3.up * 0.5f;
        Vector3 rightOrigin = position + Vector3.right * (halfSize.x + _rayOffset) + Vector3.up * 0.5f;
        Vector3 leftOrigin = position + Vector3.left * (halfSize.x + _rayOffset) + Vector3.up * 0.5f;

        Gizmos.DrawRay(forwardOrigin, Vector3.forward * _rayDistance);
        Gizmos.DrawRay(backOrigin, Vector3.back * _rayDistance);
        Gizmos.DrawRay(rightOrigin, Vector3.right * _rayDistance);
        Gizmos.DrawRay(leftOrigin, Vector3.left * _rayDistance);
    }
}