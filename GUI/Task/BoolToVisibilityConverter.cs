using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ST10318880_POE1.GUI.Task
{
    // Converts a boolean value to a Visibility value for WPF data binding
    public class BoolToVisibilityConverter : IValueConverter
    {
        // If the optional 'invert' parameter is supplied, the logic is reversed
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Ensure the value is a boolean and store the result
            bool boolValue = (value is bool b && b);

            // Check if the 'invert' parameter was passed
            bool invert = parameter?.ToString()?.ToLower() == "invert";

            // Invert the boolean if requested
            if (invert)
                boolValue = !boolValue;

            // Return Visible if true, otherwise Collapsed
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        // ConvertBack is not implemented as it's not needed for one-way binding
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}
