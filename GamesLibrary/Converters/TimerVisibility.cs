using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GamesLibrary.Converters
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    class TimerVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value as string) == null ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
