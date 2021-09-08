using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace CitraDigitalAndroid
{

    public class AutoSystem
    {
        internal static ExpireStatus Expired(DateTime endDate)
        {
            var sisa = endDate.Subtract(DateTime.Now);
            if (sisa.Days > 0 && sisa.Days <= 7)
                return ExpireStatus.WillExpire;
            if (sisa.Days <= 0)
                return ExpireStatus.Expire;

            return ExpireStatus.None;
        }
    }

    class Helper
    {
      //  public static string Url { get; set; } = "http://192.168.1.8";
        public static string Url { get; set; } = "https://kimprt.com";
    }


    public class IMageSourceConverter : IValueConverter
    {
            static WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var url = Helper.Url + "/" + value.ToString();

                using (var rest = new RestService())
                {
                    var data = rest.GetStreamAsync(url).Result;
                    return ImageSource.FromStream(() => data);
                }
                
                //var byteArray = Client.DownloadData(url);




            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class MyDatePicker : DatePicker
    {
        private string _format = null;
        [Obsolete]
        public static readonly BindableProperty NullableDateProperty = 
            BindableProperty.Create<MyDatePicker, DateTime?>(p => p.NullableDate, null);

        [Obsolete]
        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        [Obsolete]
        private void UpdateDate()
        {
            if (NullableDate.HasValue) { if (null != _format) Format = _format; Date = NullableDate.Value; }
            else { _format = Format; Format = "dd/MM/yyyy"; }
        }

        [Obsolete]
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        [Obsolete]
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date") NullableDate = Date;
        }
    }
}
