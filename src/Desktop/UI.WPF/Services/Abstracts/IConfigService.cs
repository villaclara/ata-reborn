using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Services.Abstracts;

/// <summary>
/// Handle Settings for the WPF app.
/// </summary>
public interface IConfigService
{
	/// <summary>
	/// Default method, get string value from settings file.
	/// </summary>
	/// <param name="sectionName">Key for value to retrieve.</param>
	/// <returns>String value or null.</returns>
	string? GetStringValue(string sectionName);

	/// <summary>
	/// Get value representing boolean from settings file. First is called the <see cref="GetStringValue(string)"/> method.
	/// </summary>
	/// <param name="sectionName">Key for value to retrieve.</param>
	/// <returns><see langword="true"/> if the value is true, false if the value is <see langword="false"/> or null</returns>
	bool GetBooleanValue(string sectionName);

	/// <summary>
	/// Get value representing boolean from settings file. First is called the <see cref="GetStringValue(string)"/> method.
	/// </summary>
	/// <param name="sectionName">Key for value to retrieve.</param>
	/// <returns><see cref="int"/> value or 0 if missing.</returns>
	int GetIntValue(string sectionName);
	
	/// <summary>
	/// Writes the settings into settings file. 
	/// </summary>
	/// <param name="sectionName">Key for setting.</param>
	/// <param name="value">Value for setting.</param>
	/// <returns>True/False if writing was successful.</returns>
	bool WriteSectionWithValue(string sectionName, string value);
}
