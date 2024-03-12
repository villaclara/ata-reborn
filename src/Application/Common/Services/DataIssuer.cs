using Application.Common.Abstracts;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services;

public class DataIssuer : IDataIssuer
{
	public List<AppInstanceVM> GetAllApps()
	{
		throw new NotImplementedException();
	}

	public AppInstanceVM? GetAppDataByName(string name)
	{
		throw new NotImplementedException();
	}
}
