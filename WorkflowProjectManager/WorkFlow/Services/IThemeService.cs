using System;

namespace WorkflowProjectManager.WorkFlow.Services;

public interface IThemeService
{
    event EventHandler<string>? ThemeChanged;
    void ApplyTheme(string theme);
    string GetCurrentTheme();
}
