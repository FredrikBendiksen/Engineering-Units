using Engineering_Units.Models;

namespace Engineering_Units;

public static class Factoring
{
    public static IEngineeringUnits GetEngineeringUnits(MemoryLists? memory = null)
    {
        return new Controller(memory);
    }
}
