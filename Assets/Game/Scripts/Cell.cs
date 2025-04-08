using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    private ICellResident _resident;

    public CellData Data { get; private set; }
    public ICellResident Resident { get; set; }

    public void Init(CellData data)
    {
        Data = data;
    }

    public bool IsEmpty()
    {
        return _resident == null;
    }
}