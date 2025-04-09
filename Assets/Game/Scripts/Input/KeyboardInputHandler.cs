using System;
using UnityEngine;

public class KeyboardInputHandler : IInputHandler
{
    public event Action<Direction> Received;

    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Received?.Invoke(Direction.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Received?.Invoke(Direction.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Received?.Invoke(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Received?.Invoke(Direction.Left);
        }
    }
}