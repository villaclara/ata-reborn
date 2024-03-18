using Application.AppToTrack.Abstracts;
using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Director.Instance;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public class DefaultDirector : ADirector
{
	private readonly IReadData<string> _readService = new ReadDataAsStringFromFile();
	private readonly IWriteData<string> _writeService = new WriteDataStringToFile();

	private List<AppInstance> _apps = [];
	private List<IAppHandler> _handlers = [];

	public override IDataIssuer DataIssuer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public override event EventHandler? WorkDone;

	public override void AddAppToTrackedList(string processName, string? appName = null)
	{
		throw new NotImplementedException();
	}

	public override void RemoveAppFromTrackedList(string processName)
	{
		throw new NotImplementedException();
	}

	public override void Run()
	{
		_readService.RetrieveData();	
	}

	public override void RunOnceManually()
	{
		throw new NotImplementedException();
	}
}
