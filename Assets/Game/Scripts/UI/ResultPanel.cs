using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField, Range(0f, 1f)] private float _targetFadeImageAlpha;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _menuButton;

    public event Action NextLevelButtonClicked;
    public event Action MenuButtonClicked;

    public Sequence Appear()
    {
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_fadeImage.DOFade(_targetFadeImageAlpha, 0.25f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        sequence.AppendCallback(() =>
        {
            SubscribeToEvents();
        });

        sequence.SetLink(gameObject);

        return sequence;
    }

    protected virtual void SubscribeToEvents()
    {
        _nextLevelButton.onClick.AddListener(OnNextButtonClicked);
        _menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    protected virtual void UnsubscribeFromEvents()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextButtonClicked);
        _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
    }

    private void OnNextButtonClicked()
    {
        NextLevelButtonClicked?.Invoke();
    }

    private void OnMenuButtonClicked()
    {
        MenuButtonClicked?.Invoke();
    }
}