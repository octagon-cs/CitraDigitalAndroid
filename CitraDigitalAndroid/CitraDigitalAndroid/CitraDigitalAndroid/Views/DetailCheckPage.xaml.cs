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

        private void MyDatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }



    public class DetailCheckPageViewModel : BaseViewModel
    {
        public GroupPemeriksaan Group { get; set; }
        public Command BackCommand { get; }
        public Command ClearCompesationCommand { get; }

        public DetailCheckPageViewModel(GroupPemeriksaan obj)
        {
            this.Group= obj;
            BackCommand = new Command(BackAction);
            ClearCompesationCommand = new Command(ClearCompetationDate);
        }

        private async void ClearCompetationDate(object obj)
        {
            await Task.Delay(200);
            var picker = obj as MyDatePicker;
            if (picker != null)
            {
                 var hasil = picker.BindingContext as HasilPemeriksaan;
                hasil.CompensationDeadline = null;
                picker.NullableDate = null;
            }
        }

        private async void BackAction(object obj)
        {
            var dataFalse =Group.Items.Where(x => !x.Hasil).FirstOrDefault();
            Group.Complete= dataFalse == null ? true : false;
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}