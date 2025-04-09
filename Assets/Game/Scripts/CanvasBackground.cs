using UnityEngine;

public class CanvasBackground : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
        _canvas.worldCamera ??= Camera.main;
    }
}