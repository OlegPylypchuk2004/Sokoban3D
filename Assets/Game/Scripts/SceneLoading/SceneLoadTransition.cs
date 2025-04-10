using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadTransition : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

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
        _fadeImage.gameObject.SetActive(true);

        _fadeImage.DOFade(1f, 0.25f)
            .From(0f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    private void OnLoadCompleted()
    {
        _fadeImage.DOFade(0f, 0.25f)
            .From(1f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                _fadeImage.gameObject.SetActive(false);
            });
    }
}