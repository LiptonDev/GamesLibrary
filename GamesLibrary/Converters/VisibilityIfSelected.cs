using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GamesLibrary.Converters
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    class VisibilityIfSelected : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (int)value > -1 ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
