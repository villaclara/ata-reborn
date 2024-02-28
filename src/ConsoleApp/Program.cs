// See https://aka.ms/new-console-template for more information
using Application.Abstracts;
using Application.Interactors;
using Application.Services;
using System.Diagnostics;

Console.WriteLine("Hello, World!");


IInteractor appinteractor = new Interactor();
IAppHandler appHandler = new AppHandler(appinteractor);


Console.ReadLine();