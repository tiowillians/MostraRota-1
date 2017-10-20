using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MostraRota.CustomControls
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty RouteCoordinatesProperty =
                BindableProperty.Create(nameof(RouteCoordinates),   // nome da BindableProperty
                                        typeof(List<Position>),     // tipo da propriedade
                                        typeof(CustomMap),          // tipo do objeto declarante
                                        new List<Position>(),       // valor default
                                        BindingMode.TwoWay);        // BindingMode para usar em SetBinding(), se nenhum BindingMode for dado. Parâmetro opcional. Valor default é BindingMode.OneWay

        public List<Position> RouteCoordinates
        {
            get { return (List<Position>)GetValue(RouteCoordinatesProperty); }
            set { SetValue(RouteCoordinatesProperty, value); }
        }
    }
}
