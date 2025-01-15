using UnityEngine;

public class Skin : MonoBehaviour
{
    public Color Color => _skinColor;

    [SerializeField] private Color _skinColor;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _cost;
    [field: SerializeField] public bool WasBought { get; private set; }
    public int Cost { get; private set; }
    public bool IsActive { get; private set; }

    public void Initialize(ShopConfig shopConfig)
    {
        Cost = shopConfig.CostStandardSkin;
    }

    public void MarkAsBought()
    {
        WasBought = true;
        _cost.SetActive(false);
    }

    public void DisableBackground()
    {
        _background.SetActive(false);
    }

    public void EnableBackground()
    {
        _background.SetActive(true);
    }

    public void MarkAsActive()
    {
        IsActive = true;
    }

    public void MarkSkinAsInactive()
    {
        IsActive = false;
    }

    public void ApplyData(SkinSaveData skinSaveData)
    {
        WasBought = skinSaveData.WasBought;
        IsActive = skinSaveData.IsActive;
    }
}