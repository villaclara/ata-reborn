using Application.Common.Abstracts;
using Application.Models;
using Application.Utilities;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services;

public class DataIssuer(IReadData readData) : IDataIssuer
{
	private readonly IReadData _readData = readData;
	
	public List<AppInstanceVM> GetAllApps()
	{
		var appsList = _readData.RetrieveData()!;

		var appsVM = new List<AppInstanceVM>();
		foreach(var app in appsList)
		{
			appsVM.Add(MyMapService.Map<AppInstance, AppInstanceVM>(app)!);
		}

		return appsVM;
	}

	public AppInstanceVM? GetAppDataByName(string name)
	{
		var appsList = _readData.RetrieveData();

		if (appsList.Count == 0)
		{
			return null;
		}
		var appVM = MyMapService.Map<AppInstance, AppInstanceVM>(
			appsList.Where(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault());
		

		return appVM;

	}
}
