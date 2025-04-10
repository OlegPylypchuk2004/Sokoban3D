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

        _returnMoveManager.MovesCountChanged -= OnMovesCountChanged;
    }

    public void Init(ReturnMoveManager returnMoveManager)
    {
        _returnMoveButton.interactable = false;

        _returnMoveManager = returnMoveManager;
        _returnMoveManager.MovesCountChanged += OnMovesCountChanged;
    }

    private void OnReturnMoveButtonClicked()
    {
        if (_returnMoveManager == null)
        {
            return;
        }

        _returnMoveManager.Return();
    }

    private void OnMovesCountChanged(int count)
    {
        _returnMoveButton.interactable = count > 0;
    }
}