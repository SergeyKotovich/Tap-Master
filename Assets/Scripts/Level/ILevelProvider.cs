using System;

public interface ILevelsProvider
{
    public event Action<int> LevelOpened; 
    public int Levels { get; }
}