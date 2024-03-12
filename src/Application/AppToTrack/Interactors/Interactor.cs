using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Interactors;


public class Interactor(AppInstance app) : IInteractor
{
    private readonly AppInstance _appInstance = app;

    public AppInstance GetAppInstace() => _appInstance;

}
