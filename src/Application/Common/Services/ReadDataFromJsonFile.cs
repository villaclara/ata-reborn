using Application.Common.Abstracts;
using Application.Models;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services;

public class ReadDataFromJsonFile : IReadData
{
    /// <summary>
    /// Inherited method. \n Using Streamreader gets the info from <see cref="ConstantValues.MAIN_FILE_NAME"/> file.
    /// </summary>
    /// <returns>Returns read data as string.</returns>
    public List<AppInstance> RetrieveData()
    {
        Log.Information("\n");
        Log.Information("{@Method} - Start executing from file {@File}", nameof(RetrieveData), ConstantValues.MAIN_FILE_NAME);

        if (!File.Exists(ConstantValues.MAIN_FILE_NAME))
        {
            Log.Warning("File {@File} does not exists, creating new file", ConstantValues.MAIN_FILE_NAME);
            File.Create(ConstantValues.MAIN_FILE_NAME)
                .Close();
            
            Log.Information("{@Method} - return empty list", nameof(RetrieveData));
            return [];
        }

        using var sr = new StreamReader(ConstantValues.MAIN_FILE_NAME);
        var read = sr.ReadToEnd();
        sr.Close();

        Log.Information("{@Method} - data was read from {@File}", nameof(RetrieveData), ConstantValues.MAIN_FILE_NAME);

        Log.Information("{@Method} - Converting data to List of AppInstance", nameof(RetrieveData));

        var apps = AppsJsonStringConverter.ConvertJsonToApps(read);
        Log.Information("{@Method} - Data - {@Apps}", nameof(RetrieveData), apps);

        return apps;
    }



    // to do
    // check if the file could not properly be read and then use backup file
    // also exception handle
    // logging
}
