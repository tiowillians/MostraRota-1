using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using MostraRota.iOS.Implementations;
using CoreLocation;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(DeviceSpecific_iOS))]
namespace MostraRota.iOS.Implementations
{
    public class DeviceSpecific_iOS : IDeviceSpecific
    {
        public void AtivaGPS()
        {
            // a chave "NSLocationWhenInUseUsageDescription" deve, obrigatoriamente, estar em Info.plist
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    NSString settingsString = UIApplication.OpenSettingsUrlString;
                    NSUrl url = new NSUrl(settingsString);
                    UIApplication.SharedApplication.OpenUrl(url);
                }
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