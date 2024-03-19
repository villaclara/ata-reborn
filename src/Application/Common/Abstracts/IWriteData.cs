using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

/// <summary>
/// Interface for writing the data of type T to any source.
/// </summary>
public interface IWriteData
{
    /// <summary>
    /// Write Data to the resource.
    /// </summary>
    /// <param name="apps">List of apps to be written.</param>
    /// <returns>Returns bool value indicating whether writing was successfull.</returns>
    bool WriteData(List<AppInstance> apps);
}
