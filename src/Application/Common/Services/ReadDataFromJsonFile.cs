using Application.Common.Abstracts;
using Application.Models;
using Application.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        Log.Information("{@Method} - Read from {@File}", nameof(RetrieveData), ConstantValues.MAIN_FILE_NAME);

        // Logic - 
        // 1. Check if main file exists - if no - we create file and return empty list.
        // 2. Yes - Try to read data from main file 
        // 3. Try to convert data to List<AppInstance>
        // 4. If error, we try to read data from backupfile
        // 5. Check if the backupfile exists - if no - we create file and return empty list.
        // 5. Try to convert data to List
        // 6. If error - we just return empty list

        List<AppInstance> apps = [];

        try
        {             
            var read = ReadFromFile(ConstantValues.MAIN_FILE_NAME);
            apps = GetListFromStringData(read);

            Log.Information("{@Method} - Data - {@Apps}", nameof(RetrieveData), apps);
            return apps;
        }
        catch (Exception)
        {
            Log.Warning("{@Method} - Error with {@File}. Trying backup file now.", nameof(RetrieveData), ConstantValues.MAIN_FILE_NAME);
            
			apps = ReadFromBackup();
		}

        Log.Information("{@Method} - Data returned - {@apps}", nameof(RetrieveData), apps);
        return apps;
    }


	private string? ReadFromFile(string filename)
	{
		if (!File.Exists(filename))
		{
			Log.Warning("{@Method} - File {@File} does not exists, creating new file.", nameof(ReadFromFile), filename);
			File.Create(filename)
				.Close();

			Log.Warning("{@Method} - return NULL.", nameof(ReadFromFile));
			return null;
		}

		Log.Information("{@Method} - Reading data from file - {@File}", nameof(ReadFromFile), filename);
		try
		{
			using var sr = new StreamReader(filename);
			var read = sr.ReadToEnd();
			sr.Close();
			return read;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - Error when reading file - {@Exception}. Return NULL.", nameof(ReadFromFile), ex.Message);
			return null;
		}

	}

	private List<AppInstance> GetListFromStringData(string? data)
	{
		if (string.IsNullOrEmpty(data))
		{
			Log.Warning("{@Method} - Data - {@data} is empty.", nameof(GetListFromStringData), data);
			throw new ArgumentNullException(nameof(data));
		}

		Log.Information("{@Method} - Start Converting data.", nameof(GetListFromStringData));
		try
		{
			var apps = AppsJsonStringConverter.ConvertJsonToApps(data);
			Log.Information("{@Method} - Data - {@Apps}", nameof(GetListFromStringData), apps);

			return apps;
		}
		catch (Exception ex)
		{
			Log.Error("{@Method} - threw exception - {@Exception}. \n Data read - {@data}", nameof(AppsJsonStringConverter.ConvertJsonToApps), ex.Message, data);
			throw new JsonException();
		}
	}

	// Reads the data from the backup file
	private List<AppInstance> ReadFromBackup()
    {
        try
        {
			var readBackup = ReadFromFile(ConstantValues.BACKUP_MAIN_FILE_NAME);
            var apps = GetListFromStringData(readBackup);
			Log.Information("{@Method} - Data - {@Apps}", nameof(ReadFromBackup), apps);

            return apps;
		}
        catch(Exception)
        {
            Log.Warning("{@Method} - Backup data could not be read. Return EMPTY list.", nameof(ReadFromBackup));
            return [];
		}
	}



   

}
