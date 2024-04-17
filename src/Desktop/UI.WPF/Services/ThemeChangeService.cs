using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using UI.WPF.Enums;

namespace UI.WPF.Services;

/// <summary>
/// Interface responsible for chaning the Theme of the Application.
/// </summary>
public interface IThemeChangeService
{
	/// <summary>
	/// Retrieve the Current Theme, represented by <see cref="UIThemes"/> enum.
	/// </summary>
	UIThemes CurrentTheme { get; }
	/// <summary>
	/// Set the new theme. Pass <see cref="UIThemes"/> value or <see cref="string"/> representation of theme.
	/// </summary>
	/// <param name="theme"><see cref="UIThemes"/> value, could be <see langword="null"/>.</param>
	/// <param name="strTheme">String representation of theme to be set.</param>
	void SetTheme(UIThemes? theme = null, string? strTheme = null);
}

public class ThemeChangeService : IThemeChangeService
{

	public ThemeChangeService()
	{
		// Set the theme on Initialization.
		var theme = System.Configuration.ConfigurationManager.AppSettings["Theme"];
		CurrentTheme = theme switch
		{
			"Dark" or "dark" => UIThemes.Dark,
			"Light" or "light" => UIThemes.Light,
			_ => UIThemes.Light
		};

		SetNewTheme(CurrentTheme);

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

		SetNewTheme(theme);
	}

	private void SetNewTheme(UIThemes? theme)
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
