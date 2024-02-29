using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Timers;

/// <summary>
/// Interface that is raising an event after some time elapsed.
/// </summary>
public interface ITimerService
{
    /// <summary>
    /// TimElapsed Event that is raised. Parameter int is the time of seconds passed.
    /// </summary>
    event EventHandler<int> TimeElapsed;

}
