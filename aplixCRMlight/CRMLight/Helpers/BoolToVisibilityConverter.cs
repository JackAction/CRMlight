using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CRMLight
{
    public class BoolToVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// Konvertierung von Datentyp Bool nach Klasse Visibility. Wird in WPF
        /// benoetigt, um Visibility zu steuern.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Objekt</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Dient der Rueckkonvertierung von Klasse Visiblity zu Bool. Wird
        /// momentan nicht benoetigt. Kann bei Bedarf ausprogrammiert werden
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
