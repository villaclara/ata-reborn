﻿using Application.Models;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

/// <summary>
/// Interface for retrieving data as Lit from any resource. 
/// </summary>
public interface IReadData
{
    /// <summary>
    /// Retrieves the Data from the resource. It could be read from file, read from db etc.
    /// </summary>
    /// <returns>Object of chosen type or null value.</returns>
    List<AppInstance> RetrieveData();
}
