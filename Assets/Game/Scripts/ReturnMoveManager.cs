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

        public void Return()
        {
            if (_moveDatas.Count <= 0)
            {
                return;
            }

            Dictionary<CellResident, Cell> lastMove = _moveDatas.Last();

            if (lastMove.TryGetValue(_player, out Cell cell))
            {
                _player.CurrentCell.Resident = null;
                _player.Move(GetDirection(_player.CurrentCell, cell), MoveType.Return);

                lastMove.Remove(_player);
            }

            //foreach (KeyValuePair<CellResident, Direction> entry in lastMove)
            //{
            //    entry.Key.CurrentCell.Resident = null;
            //    entry.Key.Move(GetReverseDirection(entry.Value), MoveType.Return);
            //}

            _moveDatas.RemoveAt(_moveDatas.Count - 1);
        }

        public int MovesCount => _moveDatas.Count;

        private void OnPlayerMoved(Direction direction, Cell fromCell, Cell toCell)
        {
            Dictionary<CellResident, Cell> moveData = new Dictionary<CellResident, Cell>();
            moveData.Add(_player, fromCell);

            foreach (CellResident cellResident in _cellResidents)
            {
                moveData.Add(cellResident, fromCell);
            }

            _moveDatas.Add(moveData);
        }

        private Direction GetDirection(Cell fromCell, Cell toCell)
        {
            if (fromCell.Data.ForwardCell == toCell)
            {
                return Direction.Forward;
            }
            else if (fromCell.Data.BackCell == toCell)
            {
                return Direction.Backward;
            }
            else if (fromCell.Data.RightCell == toCell)
            {
                return Direction.Right;
            }
            else if (fromCell.Data.LeftCell == toCell)
            {
                return Direction.Left;
            }

            return Direction.Forward;
        }
    }
}