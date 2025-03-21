using Containers.exceptions;

namespace Containers.classes;

public abstract class Container
{
    public Container(int height, double ownWeight, int depth, double maxLoad, char containerType)
    {
        Height = height;
        OwnWeight = ownWeight;
        Depth = depth;
        MaxLoad = maxLoad;
        CargoMass = 0;
        var containerNumber = SystemState.Instance.UniqueNumber;
        SerialNumber = $"KON-{containerType}-{containerNumber}";
    }

    public double CargoMass { get; set; }
    public int Height { get; set; }
    public double OwnWeight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; set; }
    public double MaxLoad { get; set; }

    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }

    public virtual void LoadCargo(double mass)
    {
        if (mass > MaxLoad)
            throw new OverfillException(
                $"Załadunek {mass} kg przekracza ładowność kontenera {SerialNumber} wynoszącą {MaxLoad} kg.");

        CargoMass = mass;
    }

    public override string ToString()
    {
        return $"Kontener {SerialNumber} | Waga załadunku: {CargoMass}/{MaxLoad} kg";
    }
}