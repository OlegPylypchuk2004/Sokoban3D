using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextColorChanger : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _textMesh;

    private Color _normalColor;
    private Color _disabledColor;

    private void Start()
    {
        _normalColor = _button.colors.normalColor;
        _disabledColor = _button.colors.disabledColor;
    }

    private void LateUpdate()
    {
        _textMesh.color = _button.interactable ? _normalColor : _disabledColor;
    }
}