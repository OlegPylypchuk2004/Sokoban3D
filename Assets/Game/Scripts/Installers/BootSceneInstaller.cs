using SceneLoading;
using UnityEngine;
using Zenject;

public class BootSceneInstaller : MonoInstaller
{
    [SerializeField] private SceneLoadTransition _sceneLoadTransition;

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
            .FromInstance(_sceneLoadTransition)
            .AsSingle();
    }
}