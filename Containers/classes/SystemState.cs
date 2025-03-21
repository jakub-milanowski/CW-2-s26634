namespace Containers.classes;

public class SystemState
{
    private int _uniqueNumberCounter;

    public static SystemState Instance { get; } = new();

    public int UniqueNumber => ++_uniqueNumberCounter;
}