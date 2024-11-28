namespace Cube
{
    public interface IMover
    {
        public bool IsMoving { get; }
        public void TryMove();
    }
}