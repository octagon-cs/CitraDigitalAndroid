using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CitraDigitalAndroid
{
    class Helper
    {
        public static string Url { get; set; } = "http://192.168.1.5";
    }


    public class IMageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                return Helper.Url + "/" + value.ToString();
            }
            return string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
