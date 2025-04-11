using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SceneLoading
{
    public class SceneLoadTransition : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;

        private ISceneLoader _sceneLoader;

        private void OnDestroy()
        {
            _sceneLoader.LoadStarted -= OnLoadStarted;
            _sceneLoader.LoadCompleted -= OnLoadCompleted;
        }

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            DontDestroyOnLoad(gameObject);

            _sceneLoader.LoadStarted += OnLoadStarted;
            _sceneLoader.LoadCompleted += OnLoadCompleted;
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
}