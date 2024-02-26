// See https://aka.ms/new-console-template for more information
using Application.Abstracts;
using Application.Services;

Console.WriteLine("Hello, World!");


ITimerService timerService = new TimerService();

IAppStateChecker appStateChecker = new AppStateChecker(timerService);


Console.ReadLine();