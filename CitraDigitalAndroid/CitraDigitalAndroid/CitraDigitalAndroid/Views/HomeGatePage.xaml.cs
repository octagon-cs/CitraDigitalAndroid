﻿using CitraDigitalAndroid.Models;
using CitraDigitalAndroid.ViewModels;
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
    public partial class HomeGatePage : ContentPage
    {
        public HomeGatePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeGatePageViewModel();
        }
    }


    public class HomeGatePageViewModel : BaseViewModel
    {
        public ObservableCollection<Truck> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Truck> ItemTapped { get; }
        public Command<Truck> InfoTapped { get; }

        public HomeGatePageViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<Truck>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Truck>(OnItemSelected);
            InfoTapped = new Command<Truck>(OnInfoSelected);
            LoadItemsCommand.Execute(null);
        }

        private async void OnInfoSelected(Truck truck)
        {
            try
            {
                if (truck == null)
                    return;

                if (truck.KIM == null)
                    throw new SystemException("Truck Belum Memiliki KIM.");

               
                
                var lastHistories = await GateService.TruckHistories(truck.Id);
                
                var lastIncoming = lastHistories.LastIncomming;
                
                if (lastIncoming != null && lastIncoming.Notes != null &&
                    lastIncoming.Notes.Count > 0)
                {
                    lastIncoming.TruckId = truck.Id;
                    var pageInfo = new GateTruckLastCheckUp(lastIncoming);
                    await Shell.Current.Navigation.PushModalAsync(pageInfo);
                }else
                {
                    MessagingCenter.Send<MessagingCenterAlert>(new MessagingCenterAlert
                    {
                        Message = "Tidak Ada Info Terakhir",
                        Title = "Error",
                        Cancel = "Keluar"
                    }, "message");
                }

            }
            catch (Exception ex)
            {
                MessagingCenter.Send<MessagingCenterAlert>(new MessagingCenterAlert
                {
                    Message = ex.Message,
                    Title = "Error",
                    Cancel = "Keluar"
                }, "message");
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Task.Delay(1000);
                Items.Clear();
                var items = await GateService.Trucks();
                foreach (var item in items)
                {
                        Items.Add(item);
                }

                if (Items.Count <= 0)
                    NotFound = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                await Task.Delay(100);
                IsBusy = false;
            }
        }


        async  void OnItemSelected(Truck truck)
        {
            try
            {
                if (truck == null)
                    return;

                
                if (truck.KIM == null)
                {
                    throw new SystemException("Truck Belum memilik KIM.");
                }

                var lastItem = await GateService.TruckLastChencUp(truck.Id);
                if (lastItem == null)
                    throw new SystemException("Truck Belum Diajukan Untuk Dibuatkan KIM.");

                var page = new DetailTruckPage();
                    page.BindingContext = new DetailTruckPageViewModel(lastItem);
                    await Shell.Current.Navigation.PushAsync(page);


               
            }
            catch (Exception ex)
            {
                MessagingCenter.Send<MessagingCenterAlert>(new MessagingCenterAlert {
                 Message=ex.Message, Title="Error", Cancel="Keluar"
                }, "message");
            }
        }

        private bool notFound;

        public bool NotFound
        {
            get { return notFound; }
            set { SetProperty(ref notFound , value); }
        }

    }
}