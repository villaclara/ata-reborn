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

public class WriteDataStringToFile : IWriteData
{

    // to do
    // exception handling
    // logging
    // write to backup file as well the same data

    /// <summary>
    /// Inherited method. Writes the string to the <see cref="ConstantValues.MAIN_FILE_NAME"/> file.
    /// </summary>
    /// <param name="apps">List of <see cref="AppInstance"/> to be written.</param>
    /// <returns>Returns whether the writing was successfull.</returns>
    public bool WriteData(List<AppInstance> apps)
    {
        var strToWrite = AppsJsonStringConverter.ConvertAppsToJson(apps);

        bool bSuccess = false;
        try
        {
            Log.Information("{@Method} - Start executing", nameof(WriteData));
            using var sw = new StreamWriter(ConstantValues.MAIN_FILE_NAME, append: false);
            sw.Write(strToWrite);


            using var sw1 = new StreamWriter(ConstantValues.BACKUP_MAIN_FILE_NAME, append: false);
            sw1.Write(strToWrite);

            bSuccess = true;
            return bSuccess;

        }
        catch (Exception ex)
        {
            bSuccess = false;

            // log exception
            Log.Error("Exeption when executing {@Method} - {@Ex}", nameof(WriteData), ex);

            return bSuccess;
        }
        finally
        {
            Log.Information("{@Method} - End executing with result - {@Result}", nameof(WriteData), bSuccess);
        }

    }
}
