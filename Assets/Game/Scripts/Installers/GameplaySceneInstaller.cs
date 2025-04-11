using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameplaySceneUI _gameplaySceneUI;

    public override void InstallBindings()
    {
        Container.Bind<GameplaySceneUI>()
            .FromInstance(_gameplaySceneUI);
    }
}