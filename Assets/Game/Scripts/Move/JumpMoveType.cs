using DG.Tweening;
using UnityEngine;

namespace CellResidentMove
{
    public class JumpMoveType : IMoveType
    {
        public Tween Move(Transform transform, Vector3 position, float duration)
        {
            return transform.DOJump(position, 0.75f, 1, duration)
                .SetEase(Ease.OutQuad)
                .SetLink(transform.gameObject);
        }
    }
}