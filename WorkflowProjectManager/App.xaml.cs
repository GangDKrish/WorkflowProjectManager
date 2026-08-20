using System.Configuration;
using System.Data;
using System.Windows;
using WorkflowProjectManager.WorkFlow.Services;

namespace WorkflowProjectManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var themeService = new ThemeService();
            themeService.ApplyTheme(themeService.GetCurrentTheme());
        }
    }

}
