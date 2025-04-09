using DG.Tweening;
using UnityEngine;

namespace CellResidentMove
{
    public interface IMoveType
    {
        public Tween Move(Transform transform, Vector3 position, float duration);
    }
}