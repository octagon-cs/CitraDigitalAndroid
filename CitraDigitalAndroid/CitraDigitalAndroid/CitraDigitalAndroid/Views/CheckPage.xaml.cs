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
    public partial class CheckPage : ContentPage
    {
        public CheckPage()
        {
            InitializeComponent();
        }

        private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var vm = (CheckPageViewModel)this.BindingContext;
            vm.OnItemSelected(sender as GroupPemeriksaan);
        }
    }

    public class CheckPageViewModel : BaseViewModel
    {
        public PengajuanItem Model { get; set; }

        public ObservableCollection<GroupPemeriksaan> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<GroupPemeriksaan> ItemTapped { get; }
        public CheckPageViewModel(PengajuanItem item)
        {
            this.Model = item;
            Title = "Home";
            Items = new ObservableCollection<GroupPemeriksaan>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<GroupPemeriksaan>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            LoadItemsCommand.Execute(null);
        }

        private void OnAddItem(object obj)
        {
            throw new NotImplementedException();
        }

        public async void OnItemSelected(GroupPemeriksaan obj)
        {
            var page = new DetailCheckPage();
            page.BindingContext = new DetailCheckPageViewModel(obj);
            await Shell.Current.Navigation.PushModalAsync(page);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            await Task.Delay(1000);
            try
            {
                var items = await ApprovalService.GetPenilaian(Model.Id);
                Items.Clear();

                if(items!=null && items.Count > 0)
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
        }
    }
}