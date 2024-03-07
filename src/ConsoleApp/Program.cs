// See https://aka.ms/new-console-template for more information
using Application.Abstracts;
using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Services;
using Application.Timers;
using Application.Utilities;
using Shared.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

using IHost host = builder.Build();

IReadData<string> readFileService = new ReadDataAsStringFromFile();
var readString = readFileService.RetrieveData();


List<AppInstance> apps = AppsJsonStringConverter.ConvertJsonToApps(readString!);

//// some uptimes for the app 
//apps[0].UpTimes = new List<UpTime>() {
//	new UpTime() { Date = DateOnly.FromDateTime(new DateTime(2024, 03, 06)), Minutes = 5 },
//	new UpTime() { Date = DateOnly.FromDateTime(new DateTime(2024, 03, 07)), Minutes = 10 }
//	};


List<IAppHandler> handlers = [];



foreach (var appInstance in apps)
{
	IInteractor interactor = new Interactor(appInstance);
	handlers.Add(new AppHandler(interactor));
}

var t = StaticTimerService.GetInstance();
t.TimeElapsed += OnTimerElapsed;

await host.RunAsync();


//Console.WriteLine("Hello, World!");




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


async Task OnTimerElapsed(object? sender, int e)
{
	//var t1 = Task.Run(() => appHandler.StartTrackingApp());
	//var t2 = Task.Run(() => appHandler1.StartTrackingApp());

	//await Task.WhenAll(t1, t2);
	//   await Console.Out.WriteLineAsync("====== done ====");


	// run task for each app handler
	var tasks = new List<Task>();
	foreach(var h in handlers)
	{
		tasks.Add(Task.Run(() => h.StartTrackingApp()));
	}

	await Task.WhenAll(tasks);

	string json = AppsJsonStringConverter.ConvertAppsToJson(apps);
	IWriteData<string> writeFileService = new WriteDataStringToFile();
	bool result = writeFileService.WriteToFile(json);	

	await Console.Out.WriteLineAsync($"====== done ==== with result - {result}");
}