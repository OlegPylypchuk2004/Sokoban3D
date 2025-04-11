using SceneLoading;
using UnityEngine;
using Zenject;

public class SelectLevelSceneUI : MonoBehaviour
{
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneLoader.Load(1, 0.5f);
        }
    }
}