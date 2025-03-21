namespace Containers.classes;

public class Product
{
    public Product(string name, double requiredTemperature)
    {
        Name = name;
        RequiredTemperature = requiredTemperature;
    }

    public string Name { get; set; }
    public double RequiredTemperature { get; set; }
}