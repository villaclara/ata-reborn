using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts;

public interface IWriteData
{
	bool WriteToFile(string strToWrite);
}
