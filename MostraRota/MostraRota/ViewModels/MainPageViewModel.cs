using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MostraRota.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public double DistTotal { get; set; }
        public string IniciarParar { get; set; }
        public List<Position> Trajeto { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InformaAlteracao(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public MainPageViewModel()
        {
            DistTotal = 0.0;
            IniciarParar = "Iniciar";
        }
    }
}
