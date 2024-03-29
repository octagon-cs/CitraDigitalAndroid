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
        public Command <PengajuanItem> RejectCommand { get; }
        public Command <PengajuanItem> ApproveCommand { get; }
        public Command<GroupPemeriksaan> ItemTapped { get; }
        public CheckPageViewModel(PengajuanItem item)
        {
            this.Model = item;
            Title = "Home";
            Items = new ObservableCollection<GroupPemeriksaan>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<GroupPemeriksaan>(OnItemSelected);
            ApproveCommand = new Command<PengajuanItem>(ApproveAction);
            RejectCommand = new Command<PengajuanItem>(RejectAction);

            LoadItemsCommand.Execute(null);
        }

        private async void RejectAction(PengajuanItem obj)
        {
            if (IsBusy)
                return;
            try
            {
                object result = null;
                if (await Account.UserInRole(UserType.Gate))
                {
                    result = await GateService.Reject(Model.Id, Items.SelectMany(x => x.Items).ToList());
                }
                else
                {
                    result = await ApprovalService.Reject(Model.Id, Items.SelectMany(x => x.Items).ToList());
                }
                if (result != null)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Info",
                        Message = "Berhasil !",
                        Cancel = "OK"
                    }, "message");
                    IsBusy = false;
                    MessagingCenter.Send(Model, "approve");
                    await Shell.Current.GoToAsync($"//Home");
                }
                else
                    throw new SystemException("Gagal... !, Periksa Kembali Data Anda");

            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
                    Cancel = "OK"
                }, "message");
                IsBusy = false;
            }
        }

        private async void ApproveAction(PengajuanItem obj)
        {
            if (IsBusy)
                return;
            try
            {
                object result = null;
                if (await Account.UserInRole(UserType.Gate))
                {
                    result = await GateService.Approve(Model.Id, Items.SelectMany(x => x.Items).ToList());
                }
                else
                {
                    result = await ApprovalService.Approve(Model.Id, Items.SelectMany(x => x.Items).ToList());
                }

                if (result != null)
                {
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Info",
                        Message = "Berhasil !",
                        Cancel = "OK"
                    }, "message");
                    MessagingCenter.Send(Model, "approve");
                    await Shell.Current.GoToAsync($"//Home");
                }
                else
                    throw new SystemException("Gagal... !, Periksa Kembali Data Anda");

            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
                    Cancel = "OK"
                }, "message");
                IsBusy = false;
            }
        }

        public async void OnItemSelected(GroupPemeriksaan obj)
        {
            var page = new DetailCheckPage();
            page.BindingContext = new DetailCheckPageViewModel(obj);
            await Shell.Current.Navigation.PushModalAsync(page);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if(IsBusy)
                return;
            await Task.Delay(1000);
            try
            {
                IsBusy = true;
                Items.Clear();
                List<HasilPemeriksaan> items = null;            
                if (await Account.UserInRole(UserType.Gate))
                {
                   items= Model.HasilPemeriksaan;
                }
                else
                {
                    items = await ApprovalService.GetPenilaian(Model.Id);
                }
                if(items!=null && items.Count > 0)
                {
                    var groups = items.Where(x=>x.ItemPemeriksaan.JenisPemeriksaan== JenisPemeriksaan.Kelengkapan).GroupBy(x => x.ItemPemeriksaan.Pemeriksaan.Id);
                    foreach (var group in groups)
                    {
                        var data = group.FirstOrDefault();
                        Items.Add(new GroupPemeriksaan {  Name=data.ItemPemeriksaan.Kelengkapan,    PemeriksaanId = group.Key,Items = group.ToList() });
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