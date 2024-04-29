using Serilog;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Services.Abstracts;
using UI.WPF.Utilities;
using UI.WPF.ViewModels;

namespace UI.WPF.Services.Implementations;

public class XMLConfigService : IConfigService
{
	private readonly Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

	public bool GetBooleanValue(string sectionName)
	{
		var value = GetStringValue(sectionName);
		if(value == null )
		{
			return false;
		}

		return value.ToLower().Trim() switch
		{
			"true" => true,
			_ => false
		};
	}

	public int GetIntValue(string sectionName)
	{
		var value = GetStringValue(sectionName);
		if( value == null )
		{
			return 0;
		}
		try
		{
			return Convert.ToInt32(value);
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - error ({@err}).", nameof(GetIntValue), ex.Message);
			return 0;
		}
	}

	public string? GetStringValue(string sectionName)
	{
		return ConfigurationManager.AppSettings[sectionName];
	}

	public bool WriteSectionWithValue(string sectionName, string value)
	{
		if(string.IsNullOrEmpty(sectionName)) 
		{
			return false;
		}
		if(string.IsNullOrEmpty(value))
		{ 
			return false; 
		}

		try
		{

			if (_config.AppSettings.Settings[sectionName] is null)
			{
				_config.AppSettings.Settings.Add(sectionName, value);
			}
			else
			{
				_config.AppSettings.Settings[sectionName].Value = value;
			}

			_config.Save(ConfigurationSaveMode.Modified);

			ConfigurationManager.RefreshSection(_config.AppSettings.SectionInformation.Name);
			return true;
		}
		catch(Exception ex)
		{
			Log.Error("{@Method} - ({@ex}).", nameof(WriteSectionWithValue), ex.Message);
			return false;
		}
	}


	private void Save()
	{
	}


	
}
