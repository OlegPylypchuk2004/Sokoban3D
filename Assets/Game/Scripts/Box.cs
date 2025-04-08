using UnityEngine;

public class Box : MonoBehaviour, ICellResident
{
    [SerializeField] private Cell _currentCell;

    private void Start()
    {
        _currentCell.Resident = this;
    }
}