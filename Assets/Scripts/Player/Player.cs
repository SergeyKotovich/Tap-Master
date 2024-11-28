using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private DefaultData _defaultData;

    [SerializeField] private Wallet _wallet;
    [SerializeField] private Inventory _inventory;


    private void Awake()
    {
        _defaultData = new DefaultData();
    }

    public void TryBuy(ItemsType itemsType)
    {
        var item = _inventory.GetItemByType(itemsType);

        if (!_wallet.HasEnoughMoney(item.Price)) return;

        _wallet.SpendMoney(item.Price);
        _inventory.AddNewItem(item);
    }
}