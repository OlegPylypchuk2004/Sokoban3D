using ReturnMoveSystem;
using SceneLoading;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplaySceneUI : MonoBehaviour
{
    [SerializeField] private Button _returnMoveButton;
    [SerializeField] private Button _menuMoveButton;
    [SerializeField] private TextMeshProUGUI _movesCountTextMesh;
    [SerializeField] private ResultPanel _resultPanel;

    private ISceneLoader _sceneLoader;
    private Player _player;
    private ReturnMoveManager _returnMoveManager;

    private void OnEnable()
    {
        _returnMoveButton.onClick.AddListener(OnReturnMoveButtonClicked);
        _menuMoveButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnDisable()
    {
        _returnMoveButton.onClick.RemoveListener(OnReturnMoveButtonClicked);
        _menuMoveButton.onClick.RemoveListener(OnMenuButtonClicked);

        _returnMoveManager.MovesCountChanged -= OnMovesCountChanged;
    }

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Init(Player player, ReturnMoveManager returnMoveManager)
    {
        _player = player;

        _returnMoveManager = returnMoveManager;
        _returnMoveManager.MovesCountChanged += OnMovesCountChanged;

        _returnMoveButton.interactable = _returnMoveManager.MovesCount > 0;
        _movesCountTextMesh.text = $"Cancel move ({_returnMoveManager.MovesCount})";
    }

    public void AppearResultPanel()
    {
        _resultPanel.Appear();
    }

    private void OnReturnMoveButtonClicked()
    {
        if (_returnMoveManager == null)
        {
            return;
        }

        _returnMoveManager.Return();
    }

    private void OnMenuButtonClicked()
    {
        _sceneLoader.Load(1, 0.5f);
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