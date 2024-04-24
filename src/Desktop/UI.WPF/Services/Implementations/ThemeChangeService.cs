using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using UI.WPF.Enums;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.Services.Implementations;

public class ThemeChangeService : IThemeChangeService
{

    public ThemeChangeService()
    {
        // Set the theme on Initialization.
        var theme = ConfigurationManager.AppSettings["Theme"];
        CurrentTheme = theme switch
        {
            "Dark" or "dark" => UIThemes.Dark,
            "Light" or "light" => UIThemes.Light,
            _ => UIThemes.Light
        };

        SetThemeInUI(CurrentTheme);

    }

    public UIThemes CurrentTheme { get; private set; } = UIThemes.Light;
    public void SetTheme(UIThemes? theme = null, string? strTheme = null)
    {
        var stringTheme = theme switch
        {
            UIThemes.Light => "Light",
            UIThemes.Dark => "Dark",
            _ => "Light"
        };

        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (config.AppSettings.Settings["Theme"] is null)
        {
            config.AppSettings.Settings.Add("Theme", stringTheme);
        }
        else
        {
            config.AppSettings.Settings["Theme"].Value = stringTheme;
        }
        config.Save(ConfigurationSaveMode.Modified);

        SetThemeInUI(theme);
    }

    private void SetThemeInUI(UIThemes? theme)
    {
        string newThemePath = theme switch
        {
            UIThemes.Light => "Resources/Dictionaries/LightTheme.xaml",
            UIThemes.Dark => "Resources/Dictionaries/DarkTheme.xaml",
            _ => "Resources/Dictionaries/LightTheme.xaml"
        };

        var newTheme = (ResourceDictionary)System.Windows.Application.LoadComponent(new Uri(newThemePath, UriKind.Relative));
        System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
        System.Windows.Application.Current.Resources.MergedDictionaries.Add(newTheme);
    }
}
