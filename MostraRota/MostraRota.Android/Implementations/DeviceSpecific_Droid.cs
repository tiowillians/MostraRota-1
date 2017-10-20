using Android.Content;
using MostraRota.Droid.Implementations;
using Android.Locations;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceSpecific_Droid))]
namespace MostraRota.Droid.Implementations
{
    public class DeviceSpecific_Droid : IDeviceSpecific
    {
        //
        // calcula distância (em metros) entre duas coordenadas geográficas
        //
        public double CalculaDistancia(double startLat, double startLng, double endLat, double endLng)
        {
            float[] results = new float[1];
            Location.DistanceBetween(startLat, startLng, endLat, endLng, results);
            return results[0];
        }

        public void AtivaGPS()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);
            }
        }
    }
}