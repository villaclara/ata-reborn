using Application.Abstracts;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class WriteDataToFileService : IWriteData
{

	// to do
	// exception handling
	// logging
	// write to backup file as well the same data

	public bool WriteToFile(string strToWrite)
	{
		File.WriteAllText(ConstantValues.MAIN_FILE_NAME, strToWrite);
		return true;
	}
}
