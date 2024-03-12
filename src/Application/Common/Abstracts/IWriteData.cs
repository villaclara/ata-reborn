using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

/// <summary>
/// Interface for writing the data of type T to any source.
/// </summary>
/// <typeparam name="T">The Type of the data to be written.</typeparam>
public interface IWriteData<T>
{
    /// <summary>
    /// Write Data to the resource.
    /// </summary>
    /// <param name="toWrite">Data of type T to be written.</param>
    /// <returns>Returns bool value indicating whether writing was successfull.</returns>
    bool WriteToFile(T toWrite);
}
