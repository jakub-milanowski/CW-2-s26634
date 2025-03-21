using Containers.classes;
using Containers.exceptions;

Console.WriteLine("Statki z kontenerami");

var banana = new Product("Banana", -2);
var iceCream = new Product("Ice Cream", -18);
var fish = new Product("Fish", 2);

var fluidContainer = new FluidContainer(250, 1500, 600, 20000, true);
var gasContainer = new GasContainer(220, 2000, 550, 15000, 2.5);
var freezerContainer = new FreezerContainer(260, 3000, 650, 18000, -5, banana);

try
{
    fluidContainer.LoadCargo(9000);
    gasContainer.LoadCargo(10000);
    freezerContainer.LoadCargo(15000);
}
catch (OverfillException exception)
{
    Console.WriteLine($"Błąd: {exception.Message}");
}

Console.WriteLine("\nInformacje o kontenerach:");
Console.WriteLine(fluidContainer);
Console.WriteLine();
Console.WriteLine(gasContainer);
Console.WriteLine();
Console.WriteLine(freezerContainer);

Console.WriteLine("\nTworzenie statku.");
var ship = new Ship("Kontenerowiec-1", 25, 100, 5000);

Console.WriteLine("\nŁadowanie kontenerów na statek.");
ship.LoadContainer(fluidContainer);
ship.LoadContainer(gasContainer);
ship.LoadContainer(freezerContainer);

ship.DisplayShipInfo();

Console.WriteLine("\nRozładunek kontenera na gaz.");
gasContainer.EmptyCargo();
Console.WriteLine(gasContainer);

Console.WriteLine("\nUsuwanie kontenera ze statku.");
ship.RemoveContainer(fluidContainer.SerialNumber);

ship.DisplayShipInfo();

Console.WriteLine("\nTworzenie drugiego statku.");
var ship2 = new Ship("Kontenerowiec-2", 30, 150, 8000);
Console.WriteLine($"Utworzono statek: {ship2.Name}");

Console.WriteLine("\nTworzenie dodatkowych kontenerów:");
var normalFluidContainer = new FluidContainer(240, 1300, 580, 18000, false);
var freezerContainer2 = new FreezerContainer(250, 2800, 630, 16000, -20, iceCream);
var fishContainer = new FreezerContainer(245, 2500, 620, 17000, 1, fish);

Console.WriteLine($"Utworzono kontener na płyny: {normalFluidContainer.SerialNumber}");
Console.WriteLine($"Utworzono kontener chłodniczy: {freezerContainer2.SerialNumber}");
Console.WriteLine($"Utworzono kontener chłodniczy: {fishContainer.SerialNumber}");

try
{
    normalFluidContainer.LoadCargo(16000);
    freezerContainer2.LoadCargo(10000);
    fishContainer.LoadCargo(12000);

    Console.WriteLine("\nZaładowano ładunki do nowych kontenerów.");
}
catch (OverfillException exception)
{
    Console.WriteLine($"Błąd: {exception.Message}");
}

Console.WriteLine("\nŁadowanie kontenerów na drugi statek.");
ship2.LoadContainer(normalFluidContainer);
ship2.LoadContainer(freezerContainer2);

ship2.DisplayShipInfo();

Console.WriteLine("\nZaładowanie listy kontenerów na statek:");
var containerList = new List<Container> { fishContainer };
ship2.LoadContainers(containerList);
ship2.DisplayShipInfo();

Console.WriteLine("\nZastępowanie kontenera na statku:");
var newGasContainer = new GasContainer(230, 1800, 560, 14000, 3.0);
newGasContainer.LoadCargo(7000);
Console.WriteLine($"Utworzono nowy kontener gazowy: {newGasContainer.SerialNumber}");

try
{
    ship.ReplaceContainer(gasContainer.SerialNumber, newGasContainer);
    Console.WriteLine(
        $"Zastąpiono kontener {gasContainer.SerialNumber} nowym kontenerem {newGasContainer.SerialNumber}");
}
catch (ArgumentException exception)
{
    Console.WriteLine($"Błąd: {exception.Message}");
}

ship.DisplayShipInfo();

Console.WriteLine("\nRozładunek wybranego kontenera:");
ship.UnloadContainer(freezerContainer.SerialNumber);
Console.WriteLine($"Rozładowano kontener {freezerContainer.SerialNumber}");

Console.WriteLine("\nWyświetlenie informacji o konkretnym kontenerze:");
ship.DisplayContainerInfo(freezerContainer.SerialNumber);

Console.WriteLine("\nDemonstracja obsługi sytuacji wyjątkowych:");

var hazardousContainer = new FluidContainer(230, 1400, 570, 12000, true);
try
{
    hazardousContainer.LoadCargo(7000); // Próba załadunku powyżej 50% dla niebezpiecznego ładunku
    Console.WriteLine("Załadowano kontener z niebezpiecznym ładunkiem");
}
catch (OverfillException exception)
{
    Console.WriteLine($"Oczekiwany błąd: {exception.Message}");
}

// Próba zmiany statusu ładunku na niebezpieczny gdy załadunek przekracza 50%
try
{
    normalFluidContainer.SetCargoHazardStatus(true);
    Console.WriteLine("Zmieniono status ładunku na niebezpieczny");
}
catch (InvalidOperationException exception)
{
    Console.WriteLine($"Oczekiwany błąd: {exception.Message}");
}

Console.WriteLine("\nProgram zakończony pomyślnie.");