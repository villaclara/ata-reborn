using Application.Abstracts;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class WriteDataStringToFile : IWriteData<string>
{

	// to do
	// exception handling
	// logging
	// write to backup file as well the same data

	/// <summary>
	/// Inherited method. Writes the string to the <see cref="ConstantValues.MAIN_FILE_NAME"/> file.
	/// </summary>
	/// <param name="strToWrite">Value to be written.</param>
	/// <returns>Returns whether the write was successfull.</returns>
	public bool WriteToFile(string strToWrite)
	{
		File.WriteAllText(ConstantValues.MAIN_FILE_NAME, strToWrite);
		return true;
	}
}
