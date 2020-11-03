using CitraDigitalAndroid.Models;
using CitraDigitalAndroid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CitraDigitalAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailCheckPage : ContentPage
    {
        public DetailCheckPage()
        {
            InitializeComponent();
        }
    }



    public class DetailCheckPageViewModel : BaseViewModel
    {
        public GroupPemeriksaan Group { get; set; }
        public Command BackCommand { get; }

        public DetailCheckPageViewModel(GroupPemeriksaan obj)
        {
            this.Group= obj;
            BackCommand = new Command(BackAction);
        }

        private async void BackAction(object obj)
        {
            var dataFalse =Group.Items.Where(x => !x.Hasil).FirstOrDefault();
            Group.Complete= dataFalse == null ? true : false;
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}