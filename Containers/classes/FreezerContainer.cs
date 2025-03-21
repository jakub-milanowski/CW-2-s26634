namespace Containers.classes;

public class FreezerContainer : Container
{
    public FreezerContainer(int height, double ownWeight, int depth, double maxLoad,
        double temperature, Product storedProductType)
        : base(height, ownWeight, depth, maxLoad, 'C')
    {
        if (storedProductType.RequiredTemperature < temperature)
            throw new ArgumentException("Temperatura musi być niższa niż ta wymagana przez rodzaj produktu");
        Temperature = temperature;
        StoredProductType = storedProductType;
    }

    public Product StoredProductType { get; private set; }
    public double Temperature { get; set; }
}