using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadTransition : MonoBehaviour
{
    [SerializeField] private Image _image;

    private ISceneLoader _sceneLoader;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _sceneLoader.LoadStarted += OnLoadStarted;
        _sceneLoader.LoadCompleted += OnLoadCompleted;
    }

    private void OnDestroy()
    {
        _sceneLoader.LoadStarted -= OnLoadStarted;
        _sceneLoader.LoadCompleted -= OnLoadCompleted;
    }

    public void Init(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void OnLoadStarted()
    {
        _image.gameObject.SetActive(true);
        _image.DOFade(1f, 0.25f);
    }

    private void OnLoadCompleted()
    {
        _image.DOFade(0f, 0.25f)
            .OnComplete(() =>
            {
                _image.gameObject.SetActive(false);
            });
    }
}