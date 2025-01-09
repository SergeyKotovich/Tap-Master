public interface IMoneyRestorer
{
    public int Money { get; }
    public void Initialize(int money);
}