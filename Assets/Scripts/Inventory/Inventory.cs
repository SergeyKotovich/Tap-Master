using System.Collections.Generic;
using System.Linq;

public class Inventory : IInventoryHandler
{
    public List<Booster> Boosters { get; private set; }

    public void Initialize(List<Booster> boosters)
    {
        Boosters = boosters;
    }
    public void AddBooster(Booster booster)
    {
        if (Boosters.Any(item => booster.Type == item.Type))
        {
            booster.AddBooster();
        }
    }
}