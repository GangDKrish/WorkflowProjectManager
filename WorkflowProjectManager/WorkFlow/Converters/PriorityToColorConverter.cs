using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Converters;

public class PriorityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Priority priority)
        {
            return Brushes.Gray;
        }

        return priority switch
        {
            Priority.Low => Brushes.SeaGreen,
            Priority.Medium => Brushes.Goldenrod,
            Priority.High => Brushes.DarkOrange,
            Priority.Critical => Brushes.Firebrick,
            _ => Brushes.Gray
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
