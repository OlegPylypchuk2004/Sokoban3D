using DG.Tweening;
using UnityEngine;

public class FieldAppearAnimator : IFieldAppearAnimator
{
    public Sequence Appear(Cell[] cells, GameObject linkGameObject)
    {
        foreach (Cell cell in cells)
        {
            cell.transform.localScale = Vector3.zero;
        }

        Sequence sequence = DOTween.Sequence();

        foreach (Cell cell in cells)
        {
            sequence.Join(cell.transform.DOScale(1f, 0.25f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.05f));
        }

        sequence.SetLink(linkGameObject);

        return sequence;
    }
}