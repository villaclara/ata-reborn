using System.IO;
using System.Text.Json;
using Serilog;
using Shared.Models;
using UI.WPF.Services.Abstracts;
using UI.WPF.Utilities;

namespace UI.WPF.Services.Implementations;

public class JSONConfigService : IConfigService
{
	private readonly List<SingleSetting> _settings;

	private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };
	public JSONConfigService()
	{
		string read;
		if (!File.Exists(CValues.SettingsFile))
		{
			Log.Warning("{@Method} - File {@File} does not exists, creating new file.", nameof(JSONConfigService), CValues.SettingsFile);
			File.Create(CValues.SettingsFile)
				.Close();

			_settings = [];
			return;
		}

		Log.Information("{@Method} - Reading data from file - {@File}", nameof(JSONConfigService), CValues.SettingsFile);
		try
		{
			using var sr = new StreamReader(CValues.SettingsFile);
			read = sr.ReadToEnd();
			sr.Close();

			_settings = JsonSerializer.Deserialize<List<SingleSetting>>(read) ?? [];
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Error when reading file - {@Exception}. Return NULL.", nameof(JSONConfigService), ex.Message);
			_settings = [];
		}
	}


	public bool GetBooleanValue(string sectionName)
	{
		var value = GetStringValue(sectionName);
		if (value == null)
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
		if (value == null)
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
		return _settings.Where(s => s.Name == sectionName).FirstOrDefault()?.Value;
	}

	public bool WriteSectionWithValue(string sectionName, string value)
	{
		if (string.IsNullOrEmpty(sectionName))
		{
			return false;
		}
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}

		try
		{
			var sseting = _settings.Find(s => s.Name == sectionName);

			if (sseting == null)
			{
				_settings.Add(new SingleSetting { Name = sectionName, Value = value });
			}
			else
			{
				sseting.Value = value;
			}

			// write to json
			string json = JsonSerializer.Serialize(_settings, _jsonSerializerOptions);

			Log.Information("{@Method} - Start executing", nameof(WriteSectionWithValue));
			using var sw = new StreamWriter(CValues.SettingsFile, append: false);
			sw.Write(json);

			return true;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - ({@ex}).", nameof(WriteSectionWithValue), ex.Message);
			return false;
		}
	}
}
