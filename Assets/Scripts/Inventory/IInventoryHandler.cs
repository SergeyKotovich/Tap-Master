using System.Collections.Generic;

public interface IInventoryHandler
{
    public List<Booster> Boosters { get; }
    void Initialize(List<Booster> boosters);
    void AddBooster(Booster booster);

}