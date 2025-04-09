using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ReturnMoveSystem
{
    public class ReturnMoveManager : IDisposable
    {
        private List<Dictionary<CellResident, Direction>> _moveDatas;

        private readonly Player _player;
        private readonly CellResident[] _cellResidents;

        public ReturnMoveManager(Player player, CellResident[] cellResidents)
        {
            _moveDatas = new List<Dictionary<CellResident, Direction>>();

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

            Dictionary<CellResident, Direction> lastMove = _moveDatas.Last();

            if (lastMove.TryGetValue(_player, out Direction direction))
            {
                _player.CurrentCell.Resident = null;
                _player.Move(GetReverseDirection(direction), MoveType.Return);
            }

            _moveDatas.RemoveAt(_moveDatas.Count - 1);
        }

        public int MovesCount => _moveDatas.Count;

        private void OnPlayerMoved(Direction direction)
        {
            Dictionary<CellResident, Direction> moveData = new Dictionary<CellResident, Direction>();
            moveData.Add(_player, direction);

            foreach (CellResident cellResident in _cellResidents)
            {
                moveData.Add(cellResident, direction);
            }

            _moveDatas.Add(moveData);
        }

        private Direction GetReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    return Direction.Backward;

                case Direction.Backward:
                    return Direction.Forward;

                case Direction.Right:
                    return Direction.Left;

                case Direction.Left:
                    return Direction.Right;
            }

            return Direction.Forward;
        }
    }
}