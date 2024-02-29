using Application.AppToTrack.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Abstracts;

public interface IAppHandler
{
    IInteractor Interactor { get; }

    void StartTrackingApp();
}
