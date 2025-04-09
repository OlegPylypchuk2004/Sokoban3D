using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnMoveSystem
{
    public class ReturnMoveManager : IDisposable
    {
        private List<Dictionary<CellResident, Cell>> _moveDatas;

        private readonly Player _player;
        private readonly CellResident[] _cellResidents;

        public ReturnMoveManager(Player player, CellResident[] cellResidents)
        {
            _moveDatas = new List<Dictionary<CellResident, Cell>>();

            _player = player;
            _cellResidents = cellResidents.Where(resident => resident != player).ToArray();

            _player.Moved += OnPlayerMoved;
        }

        public void Dispose()
        {
            _player.Moved -= OnPlayerMoved;
        }

        public int MovesCount => _moveDatas.Count;

        private void OnPlayerMoved(Direction direction)
        {
            Dictionary<CellResident, Cell> moveData = new Dictionary<CellResident, Cell>();
            moveData.Add(_player, _player.CurrentCell);

            foreach (CellResident cellResident  in _cellResidents)
            {
                moveData.Add(cellResident, cellResident.CurrentCell);
            }

            _moveDatas.Add(moveData);
        }
    }
}