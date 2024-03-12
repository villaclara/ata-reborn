using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

/// <summary>
/// Interface for giving the <see cref="AppInstanceVM"/> object to the interested classes. 
/// Any client that want the data to be displayed should retrieve data from here.
/// </summary>
public interface IDataIssuer
{
	/// <summary>
	/// Retrieve <see cref="AppInstanceVM"/> object information by given name.
	/// </summary>
	/// <param name="name">Name of the Application to get.</param>
	/// <returns><see cref="AppInstanceVM"/> object if there is any.</returns>
	AppInstanceVM? GetAppDataByName(string name);

	/// <summary>
	/// Retrieve List of all <see cref="AppInstanceVM"/> objects.
	/// </summary>
	/// <returns>List of <seealso cref="AppInstanceVM"/>.</returns>
	List<AppInstanceVM> GetAllApps();
}
