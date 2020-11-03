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
    public partial class CheckSell : Xamarin.Forms.Shell
    {
        public CheckSell()
        {
            InitializeComponent();
            this.BindingContext = new CheckShellViewModel();
        }
    }

    public class CheckShellViewModel   :BaseViewModel
    {
        public CheckShellViewModel()
        {
            _menuItems = new ObservableCollection<ShellSection>();

            var section = new ShellSection
            {
                Title = "My title"
            };

            var content = new ShellContent
            {
                Content = new Views.DetailTruckPage()
            };

            section.Items.Add(content);
            MenuItems.Add(section);
        }

        private ObservableCollection<ShellSection> _menuItems;

        public ObservableCollection<ShellSection> MenuItems
        {
            get => _menuItems;
            set
            {
                SetProperty(ref _menuItems, value);
            }
        }
    }
}