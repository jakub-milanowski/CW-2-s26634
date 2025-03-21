using Containers.exceptions;
using Containers.interfaces;

namespace Containers.classes;

public class FluidContainer(int height, double ownWeight, int depth, double maxLoad, bool isHazardousCargo)
    : Container(height, ownWeight, depth, maxLoad, 'G'), IHazardNotifier
{
    public bool IsHazardousCargo { get; set; } = isHazardousCargo;

    public void NotifyHazard(string message, string containerSerialNumber)
    {
        Console.WriteLine($"Ostrzeżenie dla kontenera {containerSerialNumber}: {message}");
    }

    public override void LoadCargo(double mass)
    {
        var maxAllowedFill = IsHazardousCargo ? MaxLoad * 0.5 : MaxLoad * 0.9;

        if (mass > maxAllowedFill)
        {
            NotifyHazard(
                $"Próba załadunku {mass} kg przekracza dopuszczalny limit {maxAllowedFill} kg dla tego typu kontenera.",
                SerialNumber
            );

            throw new OverfillException(
                $"Nie można załadować {mass} kg. Maksymalny dozwolony załadunek dla tego kontenera wynosi {maxAllowedFill} kg."
            );
        }

        CargoMass = mass;
    }

    public void SetCargoHazardStatus(bool isHazardous)
    {
        if (isHazardous && !IsHazardousCargo && CargoMass > MaxLoad * 0.5)
        {
            NotifyHazard(
                $"Próba zmiany statusu ładunku na niebezpieczny gdy załadunek ({CargoMass} kg) przekracza 50% pojemności.",
                SerialNumber
            );
            throw new InvalidOperationException(
                "Nie można zmienić statusu ładunku na niebezpieczny, gdy kontener jest załadowany powyżej 50% pojemności."
            );
        }

        IsHazardousCargo = isHazardous;
    }
}