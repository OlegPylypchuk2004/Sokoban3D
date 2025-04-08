using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    public CellData Data { get; private set; }

    public void Init(CellData data)
    {
        Data = data;
        IsEmpty = true;
    }

    public bool IsEmpty;
}