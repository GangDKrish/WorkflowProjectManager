using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WorkflowProjectManager.WorkFlow.Services;

public class ThemeService : IThemeService
{
    private const string LightThemePath = "WorkFlow/Resources/Themes/LightTheme.xaml";
    private const string DarkThemePath = "WorkFlow/Resources/Themes/DarkTheme.xaml";
    private static readonly string ThemePreferenceFile = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "WorkflowProjectManager",
        "theme.txt");

    public event EventHandler<string>? ThemeChanged;

    public void ApplyTheme(string theme)
    {
        var themePath = theme.ToLower() switch
        {
            "dark" => DarkThemePath,
            "light" => LightThemePath,
            _ => LightThemePath
        };

        var themeDict = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Theme.xaml") == true);

        if (themeDict != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(themeDict);
        }

        var newTheme = new ResourceDictionary
        {
            Source = new Uri(themePath, UriKind.Relative)
        };

        Application.Current.Resources.MergedDictionaries.Insert(0, newTheme);

        SaveThemePreference(theme);
        ThemeChanged?.Invoke(this, theme);
    }

    public string GetCurrentTheme()
    {
        return LoadThemePreference();
    }

    private void SaveThemePreference(string theme)
    {
        try
        {
            var directory = Path.GetDirectoryName(ThemePreferenceFile);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(ThemePreferenceFile, theme);
        }
        catch
        {
            // Ignore save errors
        }
    }

    private string LoadThemePreference()
    {
        try
        {
            if (File.Exists(ThemePreferenceFile))
            {
                return File.ReadAllText(ThemePreferenceFile).Trim();
            }
        }
        catch
        {
            // Ignore load errors
        }
        return "Light";
    }
}
