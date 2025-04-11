using SceneLoading;
using System;
using UnityEngine;
using Zenject;

public class SelectLevelSceneUI : MonoBehaviour
{
    [SerializeField] private LevelButton[] _levelButtons;

    private ISceneLoader _sceneLoader;

    private void OnEnable()
    {
        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.Clicked += OnLevelButtonClicked;
        }
    }

    private void OnDisable()
    {
        foreach (LevelButton levelButton in _levelButtons)
        {
            levelButton.Clicked -= OnLevelButtonClicked;
        }
    }

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void OnLevelButtonClicked(LevelButton levelButton)
    {
        int levelNumber = Array.IndexOf(_levelButtons, levelButton) + 1;
        Debug.Log(levelNumber);

        _sceneLoader.Load(2, 0.5f);
    }
}