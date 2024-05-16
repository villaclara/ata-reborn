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
    // Every time we write to MAIN_FILE_NAME we increase the counter.
    private static uint _writeCount = 0;

    
    public bool WriteData(List<AppInstance> apps)
    {
        var strToWrite = AppsJsonStringConverter.ConvertAppsToJson(apps);

        bool bSuccess = false;
        try
        {
            Log.Information("{@Method} - Start executing", nameof(WriteData));
            using var sw = new StreamWriter(ConstantValues.MAIN_FILE_NAME, append: false);
            sw.Write(strToWrite);
            _writeCount++;
			Log.Information("{@Method} - write count ({@count}) after writing to ({@file}).", nameof(WriteData), _writeCount, ConstantValues.MAIN_FILE_NAME);


			// Write to backup only each fifth write time
			if (_writeCount == 5)
            {
                Log.Information("{@Method} - Write to backup.", nameof(WriteData));
			    using var sw1 = new StreamWriter(ConstantValues.BACKUP_MAIN_FILE_NAME, append: false);
                sw1.Write(strToWrite);
                _writeCount = 0;
				Log.Information("{@Method} - write count ({@count}) after writing to ({@file}).", nameof(WriteData), _writeCount, ConstantValues.BACKUP_MAIN_FILE_NAME);

			}

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
