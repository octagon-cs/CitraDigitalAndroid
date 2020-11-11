using CitraDigitalAndroid.Models;
using CitraDigitalAndroid.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CitraDigitalAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GateTruckLastCheckUp : ContentPage
    {
        public GateTruckLastCheckUp(PengajuanItem model)
        {
            InitializeComponent();
            BindingContext = new GateTruckLastCheckUpVieModel(model);
        }
    }



    public class GateTruckLastCheckUpVieModel : BaseViewModel
    {
        public ObservableCollection<IncommingNote> Items { get; }

        private PengajuanItem _model;

        public TruckIncomming Model { get; }
        public Command RejectCommand { get; }
        public Command ApproveCommand { get; }

        public GateTruckLastCheckUpVieModel(PengajuanItem model)
        {
            _model = model;
            Model = model.Truck.LastIncomming;
            Items = new ObservableCollection<IncommingNote>(Model.Notes);
            ApproveCommand = new Command(ApproveAction);
            RejectCommand = new Command(RejectAction);
        }
        private async void RejectAction()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void ApproveAction()
        {
            var page = new DetailTruckPage();
            page.BindingContext = new DetailTruckPageViewModel(_model);
            await Shell.Current.Navigation.PushAsync(page);
            await Shell.Current.Navigation.PopModalAsync();
        }


    }
}