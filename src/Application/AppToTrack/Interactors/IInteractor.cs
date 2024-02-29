using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppToTrack.Interactors;

/// <summary>
/// Represents the main and only way to get / update the <see cref="AppInstance"/> object.
/// </summary>
public interface IInteractor
{

	/// <summary>
	/// Retrieves the object of <see cref="AppInstance"/> to track.
	/// </summary>{
	AppInstance GetAppInstace();
}
