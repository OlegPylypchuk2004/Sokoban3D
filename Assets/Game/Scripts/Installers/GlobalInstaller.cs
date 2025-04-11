using SceneLoading;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private SceneLoadTransition _sceneLoadTransitionPrefab;

    public override void InstallBindings()
    {
        BindCoroutineManager();
        BindSceneLoader();
        BindSceneLoadTransition();
    }

    private void BindCoroutineManager()
    {
        Container.Bind<CoroutineManager>()
            .FromNewComponentOnNewGameObject()
            .AsSingle();
    }

    private void BindSceneLoader()
    {
        Container.Bind<ISceneLoader>()
            .To<CoroutineSceneLoader>()
            .AsSingle();
    }

    private void BindSceneLoadTransition()
    {
        Container.Bind<SceneLoadTransition>()
            .FromComponentInNewPrefab(_sceneLoadTransitionPrefab)
            .AsSingle()
            .NonLazy();
    }
}