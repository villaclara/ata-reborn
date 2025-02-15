using System.IO;
using System.Text.Json;
using Serilog;
using UI.WPF.Services.Abstracts;
using UI.WPF.Utilities;

namespace UI.WPF.Services.Implementations;

public class ChangelogService : IChangelogService
{
	private readonly IEnumerable<SingleVersionChangelogNote> _changelog;
	public ChangelogService()
	{
		string read;
		if (!File.Exists(CValues.ChangelogFile))
		{
			Log.Warning("{@Method} - File {@File} does not exists, creating new file.", nameof(ChangelogService), CValues.ChangelogFile);
			File.Create(CValues.ChangelogFile)
				.Close();

			_changelog = [];
			return;
		}

		Log.Information("{@Method} - Reading data from file - {@File}", nameof(ChangelogService), CValues.ChangelogFile);
		try
		{
			using var sr = new StreamReader(CValues.ChangelogFile);
			read = sr.ReadToEnd();
			sr.Close();

			_changelog = JsonSerializer.Deserialize<IEnumerable<SingleVersionChangelogNote>>(read) ?? [];
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Error when reading file - {@Exception}. Return NULL.", nameof(ChangelogService), ex.Message);
			_changelog = [];
		}
	}


	IEnumerable<SingleVersionChangelogNote> IChangelogService.GetAllChangelog()
	{
		return _changelog;
	}
}
