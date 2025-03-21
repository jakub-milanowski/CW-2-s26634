using Containers.exceptions;
using Containers.interfaces;

namespace Containers.classes;

public class GasContainer(int height, double ownWeight, int depth, double maxLoad, double pressure)
    : Container(height, ownWeight, depth, maxLoad, 'G'), IHazardNotifier
{
    public double Pressure { get; set; } = pressure;

    public void NotifyHazard(string message, string containerSerialNumber)
    {
        Console.WriteLine($"Ostrzeżenie dla kontenera {containerSerialNumber}: {message}");
    }

    public override void EmptyCargo()
    {
        var remainingCargo = CargoMass * 0.05;
        CargoMass = remainingCargo;
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaxLoad)
        {
            NotifyHazard(
                $"Próba załadunku {mass} kg przekracza maksymalną ładowność {MaxLoad} kg.",
                SerialNumber
            );
            throw new OverfillException($"Przekroczono maksymalną ładowność kontenera gazowego {SerialNumber}.");
        }

        CargoMass = mass;
    }
}