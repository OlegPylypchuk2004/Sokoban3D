using DG.Tweening;
using SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] private LevelsScrollRect _levelsScrollRect;
    [SerializeField] private RectTransform _panelsRectTransformParent;
    [SerializeField] private RectTransform[] _panelsRectTransforms;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backLevelsPanelButton;

    private ISceneLoader _sceneLoader;

    private void Start()
    {
        foreach (RectTransform panelRectTransform in _panelsRectTransforms)
        {
            panelRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }

    private void OnEnable()
    {
        _levelsScrollRect.LevelSelected += OnLevelSelected;

        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _backLevelsPanelButton.onClick.AddListener(OnBackLevelsPanelButtonClicked);
    }

    private void OnDisable()
    {
        _levelsScrollRect.LevelSelected -= OnLevelSelected;

        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _backLevelsPanelButton.onClick.RemoveListener(OnBackLevelsPanelButtonClicked);
    }

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void OnLevelSelected()
    {

    }

    private void OnPlayButtonClicked()
    {
        _panelsRectTransformParent.DOAnchorPos(Vector3.up * _panelsRectTransforms[0].sizeDelta.y, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    private void OnBackLevelsPanelButtonClicked()
    {
        _panelsRectTransformParent.DOAnchorPos(Vector3.zero, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }
}