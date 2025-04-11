using UnityEngine;

public class LevelsScrollRect : MonoBehaviour
{
    [SerializeField] private LevelsPanel[] _levelsPanels;
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private float _returnSpeed;

    private Vector2 _levelsPanelSize;

    private void Start()
    {
        foreach (LevelsPanel levelPanel in _levelsPanels)
        {
            StartCoroutine(levelPanel.Init());
        }

        _levelsPanelSize = _levelsPanels[0].Size;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }

        Vector2 contentTargetPosition = new Vector2(Mathf.Round(_contentRectTransform.anchoredPosition.x / _levelsPanelSize.x) * _levelsPanelSize.x, 0f);
        _contentRectTransform.anchoredPosition = Vector3.Lerp(_contentRectTransform.anchoredPosition, contentTargetPosition, _returnSpeed * Time.deltaTime);
    }
}