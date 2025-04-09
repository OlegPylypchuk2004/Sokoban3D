using DG.Tweening;
using UnityEngine;

public class CameraRotateAnimator : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    private void Start()
    {
        transform.DORotate(new Vector3(0f, _force, 0f), _duration)
            .From(new Vector3(0f, -_force, 0f))
            .SetEase(_ease)
            .SetLink(gameObject)
            .SetLoops(-1, LoopType.Yoyo);
    }
}