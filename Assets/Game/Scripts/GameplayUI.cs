using ReturnMoveSystem;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button _returnMoveButton;
    [SerializeField] private TextMeshProUGUI _movesCountTextMesh;

    private Player _player;
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

    public void Init(Player player, ReturnMoveManager returnMoveManager)
    {
        _player = player;

        _returnMoveManager = returnMoveManager;
        _returnMoveManager.MovesCountChanged += OnMovesCountChanged;

        _returnMoveButton.interactable = _returnMoveManager.MovesCount > 0;
        _movesCountTextMesh.text = $"Cancel move ({_returnMoveManager.MovesCount})";
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
        _returnMoveButton.interactable = false;
        _movesCountTextMesh.text = $"Cancel move ({count})";

        if (count > 0)
        {
            StartCoroutine(LockReturnMoveButton());
        }
    }

    private IEnumerator LockReturnMoveButton()
    {
        yield return new WaitWhile(() => _player.IsMoving);

        _returnMoveButton.interactable = _returnMoveManager.MovesCount > 0;
    }
}