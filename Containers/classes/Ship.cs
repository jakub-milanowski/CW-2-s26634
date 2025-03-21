namespace Containers.classes;

public class Ship(string name, double maxSpeed, int maxContainerCount, double maxTotalWeight)
{
    public string Name { get; } = name;
    public double MaxSpeed { get; } = maxSpeed;
    public int MaxContainerCount { get; } = maxContainerCount;
    public double MaxTotalWeight { get; } = maxTotalWeight;

    public List<Container> LoadedContainers { get; } = new List<Container>();

    public void LoadContainer(Container container)
    {
        if (LoadedContainers.Count >= MaxContainerCount)
            throw new ArgumentException(
                $"Nie można załadować kontenera {container.SerialNumber}. Osiągnięto maksymalną liczbę kontenerów ({MaxContainerCount}).");

        var currentWeight = LoadedContainers.Sum(c => c.CargoMass + c.OwnWeight) / 1000.0;
        var containerWeight = (container.CargoMass + container.OwnWeight) / 1000.0;

        if (currentWeight + containerWeight > MaxTotalWeight)
            throw new ArgumentException($"Nie można załadować kontenera {container.SerialNumber}. " +
                                        $"Przekroczono by maksymalną wagę {MaxTotalWeight} ton. " +
                                        $"Aktualna waga: {currentWeight} ton, waga kontenera: {containerWeight} ton.");

        LoadedContainers.Add(container);
    }

    public void LoadContainers(List<Container> containers)
    {
        containers.ForEach(c => LoadedContainers.Add(c));
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = LoadedContainers.FirstOrDefault(c => c.SerialNumber == serialNumber);

        if (container == null)
            throw new ArgumentException($"Kontener o numerze seryjnym {serialNumber} nie istnieje.");

        LoadedContainers.Remove(container);
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = LoadedContainers.FirstOrDefault(c => c.SerialNumber == serialNumber);

        if (container == null)
            throw new ArgumentException($"Kontener o numerze seryjnym {serialNumber} nie istnieje.");

        container.EmptyCargo();
    }

    public void ReplaceContainer(string oldSerialNumber, Container newContainer)
    {
        var index = LoadedContainers.FindIndex(c => c.SerialNumber == oldSerialNumber);

        if (index == -1) throw new ArgumentException($"Kontener o numerze seryjnym {oldSerialNumber} nie istnieje.");

        var currentWeight = LoadedContainers.Sum(c => c.CargoMass + c.OwnWeight) / 1000.0;
        var oldContainerWeight = (LoadedContainers[index].CargoMass + LoadedContainers[index].OwnWeight) / 1000.0;
        var newContainerWeight = (newContainer.CargoMass + newContainer.OwnWeight) / 1000.0;

        if (currentWeight - oldContainerWeight + newContainerWeight > MaxTotalWeight)
            throw new ArgumentException($"Nie można zastąpić kontenera {oldSerialNumber}. " +
                                        $"Przekroczono by maksymalną wagę {MaxTotalWeight} ton.");

        LoadedContainers[index] = newContainer;
    }


    public void DisplayContainerInfo(string serialNumber)
    {
        var container = LoadedContainers.FirstOrDefault(c => c.SerialNumber == serialNumber);

        if (container == null)
        {
            Console.WriteLine($"Nie znaleziono kontenera o numerze {serialNumber} na statku {Name}.");
            return;
        }

        Console.WriteLine(container.ToString());
    }

    public void DisplayShipInfo()
    {
        Console.WriteLine($"=== Informacje o statku {Name} ===");
        Console.WriteLine($"Maksymalna prędkość: {MaxSpeed} węzłów");
        Console.WriteLine($"Maksymalna liczba kontenerów: {MaxContainerCount}");
        Console.WriteLine($"Maksymalna waga ładunku: {MaxTotalWeight} ton");

        var containerCount = LoadedContainers.Count;
        var totalWeight = LoadedContainers.Sum(c => c.CargoMass + c.OwnWeight) / 1000.0; // w tonach
        var cargoWeight = LoadedContainers.Sum(c => c.CargoMass) / 1000.0; // w tonach

        Console.WriteLine($"Aktualna liczba kontenerów: {containerCount}/{MaxContainerCount}");
        Console.WriteLine($"Aktualna waga ładunku: {totalWeight:F2}/{MaxTotalWeight} ton");
        Console.WriteLine($"Waga samego ładunku (bez kontenerów): {cargoWeight:F2} ton");
    }
}