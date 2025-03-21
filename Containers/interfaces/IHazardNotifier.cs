namespace Containers.interfaces;

public interface IHazardNotifier
{
    void NotifyHazard(string message, string containerSerialNumber);
}