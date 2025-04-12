using DG.Tweening;
using System;
using UnityEngine;

public class LevelsScrollRect : MonoBehaviour
{
    [SerializeField] private LevelsPanel[] _levelsPanels;
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private LevelButton[] _levelButtons;

    private Vector2 _levelsPanelSize;

    public event Action LevelSelected;

    private void Start()
    {
        foreach (LevelsPanel levelPanel in _levelsPanels)
        {
            StartCoroutine(levelPanel.Init());
        }

        _levelsPanelSize = _levelsPanels[0].Size;

        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.Clicked += OnLevelButtonClicked;
        }
    }

    private void OnDestroy()
    {
        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.Clicked -= OnLevelButtonClicked;
        }
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

    private void OnLevelButtonClicked(LevelButton levelButton)
    {
        int levelNumber = Array.IndexOf(_levelButtons, levelButton) + 1;
        Debug.Log(levelNumber);

        LevelSelected?.Invoke();
        //_sceneLoader.Load(2, 0.5f);
    }
}