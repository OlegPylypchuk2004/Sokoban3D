using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private int _columnsCount;
    [SerializeField] private int _rowsCount;

    public void Init()
    {
        _rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        _gridLayoutGroup.padding.left = Mathf.RoundToInt(_rectTransform.sizeDelta.x - _gridLayoutGroup.cellSize.x * _columnsCount - _gridLayoutGroup.spacing.x);
        _gridLayoutGroup.padding.top = Mathf.RoundToInt(_rectTransform.sizeDelta.y - _gridLayoutGroup.cellSize.y * _columnsCount - _gridLayoutGroup.spacing.y);

        _gridLayoutGroup.enabled = false;
    }

    public Vector2 Size => _rectTransform.sizeDelta;
}