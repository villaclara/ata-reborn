using Application.AppToTrack.Enums;
using Application.AppToTrack.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Abstracts;

/// <summary>
/// AppInstance State Checker.
/// Gets the state - running, stopped of the AppInstance.
/// </summary>
public interface IAppStateChecker
{
    IInteractor AppInteractor { get; }

    /// <summary>
    /// Get the AppInstance State if it is running.
    /// </summary>
    AppInstanceState GetAppState();

    /// <summary>
    /// Sets the AppInstance State. 
    /// </summary>
    /// <param name="appInstanceState"> Enum option, by default it is as Stopped. </param>
    void SetAppState(AppInstanceState appInstanceState = AppInstanceState.Stopped);
}
