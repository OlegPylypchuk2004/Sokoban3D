using SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] private LevelsScrollRect _levelsScrollRect;
    [SerializeField] private Button _playButton;

    private ISceneLoader _sceneLoader;

    private void OnEnable()
    {
        _levelsScrollRect.LevelSelected += OnLevelSelected;

        _playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDisable()
    {
        _levelsScrollRect.LevelSelected -= OnLevelSelected;

        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
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
        _levelsScrollRect.Appear();
    }
}