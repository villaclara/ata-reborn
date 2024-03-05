using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts;

/// <summary>
/// Interface for retrieving data as type T from any resource. 
/// </summary>
public interface IReadData<T>
{
	/// <summary>
	/// Retrieves the Data from the resource. It could be read from file, read from db etc.
	/// </summary>
	/// <returns>Object of chosen type or null value.</returns>
	T? RetrieveData();
}
