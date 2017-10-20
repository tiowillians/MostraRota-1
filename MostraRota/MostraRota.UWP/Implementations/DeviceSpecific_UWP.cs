using MostraRota.UWP.Implementations;
using System;
using Windows.Devices.Geolocation;
using Xamarin.Forms.Maps;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceSpecific_UWP))]
namespace MostraRota.UWP.Implementations
{
    public class DeviceSpecific_UWP : IDeviceSpecific
    {
        public async void AtivaGPS()
        {
            Geolocator locationservice = new Geolocator();
            if (locationservice.LocationStatus == PositionStatus.Disabled)
            {
                var accessStatus = await Geolocator.RequestAccessAsync();
            }
        }

        public double CalculaDistancia(double startLat, double startLng, double endLat, double endLng)
        {
            // ângulo em radianos ao longo do grande círculo
            double radians = Math.Acos(Math.Sin(DegreesToRadians(startLat)) * Math.Sin(DegreesToRadians(endLat)) +
                                       Math.Cos(DegreesToRadians(startLat)) * Math.Cos(DegreesToRadians(endLat)) *
                                       Math.Cos(DegreesToRadians(startLng - endLng)));

            // Multiplica pelo raio da Terra para obter a distância atual
            Distance dist = Distance.FromKilometers(6371 * radians);
            return dist.Meters;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
