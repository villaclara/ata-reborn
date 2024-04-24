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

	public string? GetValueFromSection(string sectionName)
	{
		return ConfigurationManager.AppSettings["WindowHeight"];
	}

	public bool WriteSectionWithValues(string sectionName, string value)
	{
		if(string.IsNullOrEmpty(sectionName)) 
		{
			return false;
		}
		if(string.IsNullOrEmpty(value))
		{ 
			return false; 
		}


		if (_config.AppSettings.Settings[sectionName] is null)
		{
			_config.AppSettings.Settings.Add(sectionName, value);
		}
		else
		{
			_config.AppSettings.Settings[sectionName].Value = value;
		}

		Save();
		return true;
	}


	private void Save()
	{
		_config.Save(ConfigurationSaveMode.Modified);

		ConfigurationManager.RefreshSection(_config.AppSettings.SectionInformation.Name);
	}


	public T GetValue<T>(string sectionName)
	{
		var value = ConfigurationManager.AppSettings["WindowHeight"];
		//return typeof(T) switch
		//{
		//	int v => Convert.ToInt32(value),
		//	string => value,
		//	bool => value.ToLower() == "true"
		//};


	}
}
