using System.Windows;
using WorkflowProjectManager.WorkFlow.ViewModels;

namespace WorkflowProjectManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}