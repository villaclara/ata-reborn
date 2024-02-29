// See https://aka.ms/new-console-template for more information
using Application.Abstracts;
using Application.AppToTrack.Abstracts;
using Application.AppToTrack.Interactors;
using Application.AppToTrack.Services;
using Application.Services;
using Shared.Models;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


IAppInstanceInitialize appnew = new AppInstanceInitializer();
IInteractor appinteractor = appnew.InitializeAppInstanceToTrack();

IAppHandler appHandler = new AppHandler(appinteractor);



IInteractor appinteractor1 = AppInstanceInitializer.InitializeNewAppToTrack();

IAppHandler appHandler1 = new AppHandler(appinteractor1);

Console.ReadLine();