public interface IMoneyHandler
{
    void SpendMoney(int price);
    bool HasEnoughMoney(int price);
}