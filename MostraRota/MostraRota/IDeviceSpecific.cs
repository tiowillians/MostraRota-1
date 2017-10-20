
namespace MostraRota
{
    public interface IDeviceSpecific
    {
        double CalculaDistancia(double startLat, double startLng, double endLat, double endLng);
        void AtivaGPS();
    }
}
