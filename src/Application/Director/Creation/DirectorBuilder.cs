using Application.Common.Abstracts;
using Application.Director.Instance;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public class DirectorBuilder : IDirectorBuilder
{

	private readonly IDirector _director;
	
	public DirectorBuilder()
	{
		_director = new MainDirector();
	}
	public IDirectorBuilder AddReadService(IReadData readData)
	{
		_director.ReadDataService = readData;
		return this;
	}

	public IDirectorBuilder AddWriteService(IWriteData writeData)
	{
		_director.WriteDataService = writeData;
		return this;
	}

	public IDirectorBuilder AddIOServices(IReadData readService, IWriteData writeService)
	{
		_director.ReadDataService = readService;
		_director.WriteDataService = writeService;
		return this;
	}

	public IDirector Build()
	{
		return _director;
	}

	public IDirectorBuilder SetTimerCheckValue(int timeoutMiliseconds = 10_000)
	{
		ConstantValues.TIMER_INTERVAL_MS = timeoutMiliseconds;
		ConstantValues.TIMER_INTERVAL_M = timeoutMiliseconds / 60_000;
		return this;
	}

	public IDirectorBuilder SetWritableFile(string where = "apps.json")
	{
		if(string.IsNullOrEmpty(where))
		{
			return this;
		}

		// check extension
		var split = where.Split('.');
		var name = where;
		var ext = ".json";

		// if the Extension is .json then we remove it to later add "_backup" and only then Extension ".json"
		if (split[^1] == "json")
		{
			var index = where.LastIndexOf(".json");
			name = where.Substring(0, index);
		}


		// Add the extension .json to the filename
		ConstantValues.MAIN_FILE_NAME = name + ext;
		ConstantValues.BACKUP_MAIN_FILE_NAME = name + "_backup" + ext;

		return this;

	}
}
