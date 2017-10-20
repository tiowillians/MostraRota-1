using MostraRota.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MostraRota
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        private bool ColetandoDadosGPS { get; set; }

        private Xamarin.Forms.Maps.Position ultPosicao;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            this.BindingContext = viewModel;
        }

        private void BtnIniciarParar_Clicked(object sender, EventArgs e)
        {
            if (ColetandoDadosGPS)
            {
                ColetandoDadosGPS = false;
                viewModel.IniciarParar = "Iniciar";

                Task.Factory.StartNew(StopListening);
            }
            else
            {
                ColetandoDadosGPS = true;
                viewModel.IniciarParar = "Parar";
                viewModel.DistTotal = 0.0;

                mapVisualizacao.RouteCoordinates = new List<Xamarin.Forms.Maps.Position>();
                Task.Factory.StartNew(StartListening);
            }

            viewModel.InformaAlteracao("IniciarParar");
            viewModel.InformaAlteracao("DistTotal");
        }

        //
        // Inicia "escuta" do GPS
        //
        // GeoLocator Plugin Documentation: https://jamesmontemagno.github.io/GeolocatorPlugin/LocationChanges.html
        //
        private async Task StartListening()
        {
            // verifica se Geolocalizador já está escutando o GPS
            if (CrossGeolocator.Current.IsListening)
                return;

            // ativa GPS
            DependencyService.Get<IDeviceSpecific>().AtivaGPS();

            // inicia escuta do GPS
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 2);

            // adiciona métodos para tratamento dos eventos de mudança de posição e
            // erro na obtenção das coordenadas do GPS
            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        //
        // Evento: posição corrente do usuário foi alterada
        //
        private void PositionChanged(object sender, PositionEventArgs e)
        {
            // adiciona coordenada na lista de coordenadas do mapa
            var position = e.Position;
            Xamarin.Forms.Maps.Position pos = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var list = new List<Xamarin.Forms.Maps.Position>(mapVisualizacao.RouteCoordinates);
            list.Add(pos);
            mapVisualizacao.RouteCoordinates = list;

            mapVisualizacao.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(250.0)));

            // calcula distância entre as coordenadas
            if (list.Count > 1)
            {
                viewModel.DistTotal +=
                    DependencyService.Get<IDeviceSpecific>().CalculaDistancia(pos.Latitude, pos.Longitude,
                                                                              ultPosicao.Latitude, ultPosicao.Longitude);
                viewModel.InformaAlteracao("DistTotal");
            }

            ultPosicao = pos;
        }

        //
        // Evento: erro na obtençao da posição corrente do usuário via GPS
        //
        private void PositionError(object sender, PositionErrorEventArgs e)
        {
            // Colocar aqui evento para tratamento de erros
        }

        //
        // Encerra "escuta" do GPS
        //
        private async Task StopListening()
        {
            // verifica se geolocalizador está escutando o GPS
            if (!CrossGeolocator.Current.IsListening)
                return;

            // para de escutar o GPS
            await CrossGeolocator.Current.StopListeningAsync();

            // retira métodos para tratamento dos eventos de mudança de posição e
            // erro na obtenção das coordenadas do GPS
            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }
    }
}
