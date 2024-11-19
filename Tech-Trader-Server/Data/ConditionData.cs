using TechTrader.Models;

namespace TechTrader.Data
{
    public class ConditionData
    {
        public static List<Condition> Conditions = new()
        {
            new Condition { Id = 1, Name = "New" },
            new Condition { Id = 2, Name = "Used" },
            new Condition { Id = 3, Name = "Open-Box" }
        };
    }
}