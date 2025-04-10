using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CoroutineManager _coroutineManager;
    [SerializeField] private SceneLoadTransition _sceneLoadTransition;

    private ISceneLoader _sceneLoader;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _sceneLoader = new CoroutineSceneLoader(_coroutineManager);
        _sceneLoadTransition.Init(_sceneLoader);

        SceneManager.LoadScene(1);
    }
}