using CitraDigitalAndroid.Models;
using CitraDigitalAndroid.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CitraDigitalAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTruckPage : ContentPage
    {
        public DetailTruckPage()
        {
            InitializeComponent();
        }
    }

    public class DetailTruckPageViewModel : BaseViewModel
    {
        public PengajuanItem Model { get; set; }
        public License DriverData { get; private set; }
        public License AssDriverData { get; private set; }
        public License VehicleRegistrationData { get; private set; }
        public License KeurDLLAJRData { get; private set; }
        public Command NextCommand { get; }

        public DetailTruckPageViewModel(PengajuanItem item)
        {
            this.Model = item;
            DriverData = GetLicense(item.Truck.DriverLicense, item.Truck.DriverName, item.Truck.DriverIDCard, item.Truck.DriverPhoto);
            AssDriverData = GetLicense(item.Truck.AssdriverLicense, item.Truck.AssdriverName, item.Truck.AssdriverIDCard, item.Truck.AssdriverPhoto);
            VehicleRegistrationData = GetLicense(item.Truck.VehicleRegistration, null,null,null);
            KeurDLLAJRData = GetLicense(item.Truck.KeurDLLAJR,null,null,null);
            Title = "DATA TRUCK";
            NextCommand = new Command(nextAction);
        }

        private async void nextAction(object obj)
        {
            var form = new Views.CheckPage();
            var vm = new Views.CheckPageViewModel(Model);
            form.BindingContext = vm;
            await Shell.Current.Navigation.PushAsync(form);
        }

        private License GetLicense(string sourceString, string name, string idnumber, string photo)
        {
           if (string.IsNullOrEmpty(sourceString))
                return new License();

            var data = JsonConvert.DeserializeObject<License>(sourceString);
            var licence= new License
            {
                Name = name,
                NumberID = idnumber,
                Berlaku = data.Berlaku,
                Hingga = data.Hingga,
                Number = data.Number,
               
            };

            licence.Photo = string.IsNullOrEmpty(photo)?null: ImageSource.FromUri(new Uri(Helper.Url + "/" + photo));
            return licence;
        }


        /*private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            await Task.Delay(1000);
            try
            {
                var items = await ApprovalService.GetPenilaian(Model.Id);
                Items.Clear();

                if (items != null && items.Count > 0)
                {
                    var groups = items.GroupBy(x => x.ItemPemeriksaan.Pemeriksaan.Id);
                    foreach (var group in groups)
                    {
                        var data = group.FirstOrDefault();
                        Items.Add(new GroupPemeriksaan { PemeriksaanId = group.Key, Name = data.ItemPemeriksaan.Pemeriksaan.Name, Items = group.ToList() });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }*/
    }
}