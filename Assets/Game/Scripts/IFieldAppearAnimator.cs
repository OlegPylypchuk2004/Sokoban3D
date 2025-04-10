using DG.Tweening;
using UnityEngine;

public interface IFieldAppearAnimator
{
    public Sequence Appear(Cell[] cells, GameObject linkGameObject);
}