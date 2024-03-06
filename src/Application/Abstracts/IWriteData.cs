using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts;

public interface IWriteData<T>
{
	bool WriteToFile(T toWrite);
}
