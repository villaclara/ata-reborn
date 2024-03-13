using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstracts;

public interface IGetProcs
{
	IDictionary<string, string> GetUniqueProcesses();
}
