using UnityEngine;
using Zenject;

public class SelectLevelSceneInstaller : MonoInstaller
{
    [SerializeField] private MenuSceneUI _selectLevelSceneUI;

    public override void InstallBindings()
    {
        Container.Bind<MenuSceneUI>()
            .FromInstance(_selectLevelSceneUI);
    }
}