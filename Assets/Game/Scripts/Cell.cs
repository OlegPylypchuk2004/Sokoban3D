using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    public Cell ForwardCell { get; private set; }
    public Cell BackCell { get; private set; }
    public Cell RightCell { get; private set; }
    public Cell LeftCell { get; private set; }

    private CellNeighborFinder _neighborFinder;

    private void Awake()
    {
        LayerMask layerMask = 1 << gameObject.layer;
        _neighborFinder = new CellNeighborFinder(transform, _boxCollider, layerMask);

        FindNeighboringCells();
    }

    private void FindNeighboringCells()
    {
        ForwardCell = _neighborFinder.FindNeighbor(Vector3.forward);
        BackCell = _neighborFinder.FindNeighbor(Vector3.back);
        RightCell = _neighborFinder.FindNeighbor(Vector3.right);
        LeftCell = _neighborFinder.FindNeighbor(Vector3.left);
    }
}