using ReturnMoveSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button _returnMoveButton;

    private ReturnMoveManager _returnMoveManager;

    private void OnEnable()
    {
        _returnMoveButton.onClick.AddListener(OnReturnMoveButtonClicked);
    }

    private void OnDisable()
    {
        _returnMoveButton.onClick.RemoveListener(OnReturnMoveButtonClicked);
    }

    public void Init(ReturnMoveManager returnMoveManager)
    {
        _returnMoveManager = returnMoveManager;
    }

    private void OnReturnMoveButtonClicked()
    {
        if (_returnMoveManager == null)
        {
            return;
        }

        _returnMoveManager.Return();
        _returnMoveButton.interactable = _returnMoveManager.MovesCount > 0;
    }
}