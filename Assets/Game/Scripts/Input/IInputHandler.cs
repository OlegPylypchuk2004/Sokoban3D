using System;

public interface IInputHandler
{
    public event Action<Direction> Received;

    public void Check();
}