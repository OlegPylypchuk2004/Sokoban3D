using UnityEngine;

[SelectionBase]
public class Cell : MonoBehaviour
{
    private CellData _data;

    public void Init(CellData data)
    {
        _data = data;
    }
}