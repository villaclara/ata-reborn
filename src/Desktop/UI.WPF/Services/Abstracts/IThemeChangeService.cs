using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Enums;

namespace UI.WPF.Services.Abstracts;

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
