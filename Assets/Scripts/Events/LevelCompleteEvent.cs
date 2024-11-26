public struct LevelCompleteEvent
{
    public readonly int CubesCount;

    public LevelCompleteEvent(int cubesCount)
    {
        CubesCount = cubesCount;
    }
}