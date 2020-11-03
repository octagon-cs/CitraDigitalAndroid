using CitraDigitalAndroid.Models;
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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }
    }


    public class HomeViewModel : BaseViewModel
    {
        private PengajuanItem _selectedItem;

        public ObservableCollection<PengajuanItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<PengajuanItem> ItemTapped { get; }

        public HomeViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<PengajuanItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<PengajuanItem>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            LoadItemsCommand.Execute(null);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            await Task.Delay(1000);    
            try
            {
                var items = await ApprovalService.GetPersetujuan();
                Items.Clear();
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
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public PengajuanItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(PengajuanItem item)
        {
            if (item == null)
                return;

            var page = new DetailTruckPage();
            page.BindingContext = new DetailTruckPageViewModel(item);
            await Shell.Current.Navigation.PushAsync(page);
        }

        private bool notFound;

        public bool NotFound
        {
            get { return notFound; }
            set { SetProperty(ref notFound , value); }
        }

    }
}