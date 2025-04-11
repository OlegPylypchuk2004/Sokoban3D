using UnityEngine;
using Zenject;

public class SelectLevelSceneInstaller : MonoInstaller
{
    [SerializeField] private SelectLevelSceneUI _selectLevelSceneUI;

    public override void InstallBindings()
    {
        Container.Bind<SelectLevelSceneUI>()
            .FromInstance(_selectLevelSceneUI);
    }
}