using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WorkflowProjectManager.WorkFlow.Controls;

public class WatermarkTextBox : TextBox
{
    public static readonly DependencyProperty WatermarkProperty =
        DependencyProperty.Register(
            nameof(Watermark),
            typeof(string),
            typeof(WatermarkTextBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty WatermarkForegroundProperty =
        DependencyProperty.Register(
            nameof(WatermarkForeground),
            typeof(Brush),
            typeof(WatermarkTextBox),
            new PropertyMetadata(Brushes.Gray));

    static WatermarkTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(WatermarkTextBox),
            new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
    }

    public string Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }

    public Brush WatermarkForeground
    {
        get => (Brush)GetValue(WatermarkForegroundProperty);
        set => SetValue(WatermarkForegroundProperty, value);
    }
}
