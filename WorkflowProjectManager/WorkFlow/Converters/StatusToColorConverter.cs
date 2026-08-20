using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WorkflowProjectManager.WorkFlow.Models.Enums;

namespace WorkflowProjectManager.WorkFlow.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not WorkItemStatus status)
        {
            return Brushes.Gray;
        }

        return status switch
        {
            WorkItemStatus.Done => Brushes.ForestGreen,
            WorkItemStatus.InProgress => Brushes.DarkOrange,
            WorkItemStatus.InReview => Brushes.SteelBlue,
            WorkItemStatus.Blocked => Brushes.IndianRed,
            _ => Brushes.DimGray
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
