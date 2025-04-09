using DG.Tweening;
using UnityEngine;

namespace CellResidentMove
{
    public class SlideMoveType : IMoveType
    {
        public Tween Move(Transform transform, Vector3 position, float duration)
        {
            return transform.DOMove(position, duration)
                .SetEase(Ease.OutQuad)
                .SetLink(transform.gameObject);
        }
    }
}