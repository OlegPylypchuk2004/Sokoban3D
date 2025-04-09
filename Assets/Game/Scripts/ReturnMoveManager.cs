using System.Collections.Generic;
using System.Linq;

namespace ReturnMoveSystem
{
    public class ReturnMoveManager
    {
        private List<Dictionary<CellResident, Direction>> _moveDatas;

        private readonly int _maxReturnsCount; 

        public ReturnMoveManager(int maxReturnsCount)
        {
            _moveDatas = new List<Dictionary<CellResident, Direction>>();
            _maxReturnsCount = maxReturnsCount;
        }

        public void AddMoveData(Dictionary<CellResident, Direction> moveData)
        {
            _moveDatas.Add(moveData);

            if (_moveDatas.Count > _maxReturnsCount)
            {
                _moveDatas.RemoveAt(0);
            }
        }

        public void Return()
        {
            if (_moveDatas.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<CellResident, Direction> entry in _moveDatas.Last())
            {
                entry.Key.TryMove(GetReverseDirection(entry.Value), MoveType.Return);
            }

            _moveDatas.RemoveAt(_moveDatas.Count - 1);
        }

        public int MovesCount => _moveDatas.Count;

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