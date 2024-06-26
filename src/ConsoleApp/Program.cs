﻿// See https://aka.ms/new-console-template for more information
using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Common.Timers;
using Application.Utilities;
using Application.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.ViewModels;
using Microsoft.Extensions.Logging;
using Application.Director.Creation;
using Application.Director;

Console.WriteLine("Hello");

//HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("log.txt")
	.CreateLogger();

//using IHost host = builder.Build();


// WORKING CONSOLE

//IReadData<string> readFileService = new ReadDataAsStringFromFile();
//var readString = readFileService.RetrieveData();

//IWriteData<string> writeFileService = new WriteDataStringToFile();

//List<AppInstance> apps = AppsJsonStringConverter.ConvertJsonToApps(readString!);


//List<IAppHandler> handlers = [];


//// CREATING NEW APP
//var ap = AppInstanceCreator.CreateAppInstanceToTrack("ATA", "ATA_WPF");
//apps.Add(ap);


//foreach (var appInstance in apps)
//{
//	IInteractor interactor = new Interactor(appInstance);
//	handlers.Add(new AppHandler(interactor));
//}

//var t = StaticTimerService.GetInstance();
//t.TimeElapsed += OnTimerElapsed;

Log.Information("Start app - {@Program}", nameof(Program));


//await host.RunAsync();

var director = new DirectorBuilder()
	.AddIOServices(new ReadDataFromJsonFile(), new WriteDataStringToFile())
	.SetWritableFile("apps.json")
	.SetTimerCheckValue(6000)
	.Build();

director.WorkDone -= OnDirectorWorkDone;
director.WorkDone += OnDirectorWorkDone;

await director.RunAsync();



Console.ReadLine();


Task OnDirectorWorkDone(object? sender, int args)
{
	//var appstracked = dataIssuer.GetAllApps();
	//Log.Information("{@Method} - {@Apps}", nameof(OnWorkerDoneWork), appstracked);
	//Log.Information("On work done in console. updated list of apps");

	Log.Information("\n");
	Log.Information("{@Method} - Director work done triggered inside Program.cs \n", nameof(OnDirectorWorkDone));
	return Task.CompletedTask;
}


/// THE FLOW:
/// 
/// 1. Create list of appHandlers before getting the timer
/// 2. Read from file and create AppInstances objects
/// 3. Add AppHandler objects with each AppInstance to the List of AppHandler
/// 4. Call timer here
/// 5. After timer elapsed do the task for checking app
/// 6. Write to file List of AppInstances
/// 7. Repeat step 5
/// 
///
///
/// So in the main flow there should be :
/// 1. List"IAppHandler" handlers = new List();
/// 2. List"APpInstance" apps = IOService.ReadData();
/// 3. foreach(var app in apps) { handlers.Add(new Interactor(app)) };
/// 4. var timer = StaticTimerService.GetInstance();
/// 5. timer.Elapsed += OnTimerElapsed;
/// 6. OnTimerElapsed() { foreach handler create task; Task.WhenAll(); 
///    IOService.WriteData();
///    }
/// 7. repeat


/// FLOW
/// 

//IFileRead fileRead = new FileReadService();
//var readString = fileRead.ReadFromFile();


//List<AppInstance>? apps = JsonStringConverter.ConvertJsonToApps(readString);

//List<IAppHandler> handlers = new();



//foreach(var appInstance in apps)
//{
//	IInteractor interactor = new Interactor(appInstance);
//	handlers.Add(new AppHandler(interactor));
//}

//var t = StaticTimerService.GetInstance();
//t.TimeElapsed += OnTimerElapsed;


//Console.ReadLine();


//async Task OnTimerElapsed(object? sender, int e)
//{
//	if(handlers.Count == 0)
//	{
//		Log.Warning("{@Method} - {@List} of handlers is empty, nothing to do", nameof(OnTimerElapsed), nameof(List<AppHandler>));
//		return;
//	}


//	// run task for each app handler
//	var tasks = new List<Task>();
//	foreach(var h in handlers)
//	{
//		tasks.Add(Task.Run(h.StartTrackingApp));
//	}

//	await Task.WhenAll(tasks);

//	string json = AppsJsonStringConverter.ConvertAppsToJson(apps);
//	bool result = writeFileService.WriteToFile(json);

//	Log.Information("{@Method} - end", nameof(OnTimerElapsed));

//	var data = new DataIssuer(readFileService).GetAllApps();

//	Log.Information("data - {@App}", data);


//	var d = new DataIssuer(readFileService).GetAppDataByName("discord");
//	Log.Information("app - {@App}", d);
//}