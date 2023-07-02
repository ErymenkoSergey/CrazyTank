using System;

public interface IPaused
{
    public event Action<bool> OnIsPause;
}
