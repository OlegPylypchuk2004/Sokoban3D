using UnityEngine;
using ReturnMoveSystem;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CellResident[] _cellResidents;

    private ReturnMoveManager _returnMoveManager;

    private void Start()
    {
        _returnMoveManager = new ReturnMoveManager(_player, _cellResidents);
    }

    private void Update()
    {
        Debug.Log(_returnMoveManager.MovesCount);
    }
}