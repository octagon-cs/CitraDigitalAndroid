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
        public GateTruckLastCheckUp(TruckIncomming model)
        {
            InitializeComponent();
            BindingContext = new GateTruckLastCheckUpVieModel(model);
        }
    }



    public class GateTruckLastCheckUpVieModel : BaseViewModel
    {
        public ObservableCollection<IncommingNote> Items { get; }


        public TruckIncomming Model { get; }
        public Command RejectCommand { get; }
        public Command ApproveCommand { get; }

        public GateTruckLastCheckUpVieModel(TruckIncomming model)
        {
            Model = model;
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
            var lastItem = await GateService.TruckLastChencUp(Model.TruckId);
            page.BindingContext = new DetailTruckPageViewModel(lastItem);
            await Shell.Current.Navigation.PushAsync(page);
            await Shell.Current.Navigation.PopModalAsync();
        }


    }
}