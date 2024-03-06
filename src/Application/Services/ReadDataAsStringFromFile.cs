using Application.Abstracts;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class ReadDataAsStringFromFile : IReadData<string> 
{
	/// <summary>
	/// Inherited method. \n Using Streamreader gets the info from <see cref="ConstantValues.MAIN_FILE_NAME"/> file.
	/// </summary>
	/// <returns>Returns read data as string.</returns>
	public string? RetrieveData()
	{
		if(!File.Exists(ConstantValues.MAIN_FILE_NAME))
		{
			File.Create(ConstantValues.MAIN_FILE_NAME);
			return null;
		}

		using var sr = new StreamReader(ConstantValues.MAIN_FILE_NAME);
		var read = sr.ReadToEnd();
		sr.Close();
		
		return read;
	}

	

	// to do
	// check if the file could not properly be read and then use backup file
	// also exception handle
	// logging
}
