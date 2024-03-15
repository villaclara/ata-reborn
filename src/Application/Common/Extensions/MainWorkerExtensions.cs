using Application.Common.Abstracts;
using Application.Common.Services;
using Application.Models;
using Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extensions;

public static class MainWorkerExtensions
{
	//public static MainWorker Initialize(this MainWorker worker)
	//{
	//	IReadData<string> readFileService = new ReadDataAsStringFromFile();
	//	var readString = readFileService.RetrieveData();

	//	List<AppInstance> apps = AppsJsonStringConverter.ConvertJsonToApps(readString!);

	//	worker.Apps = apps;

	//	return worker;
	//}

	//public static MainWorker AddReadService<T>(this MainWorker worker, IReadData<T> readData)
	//{
	//	worker.ReadService = (IReadData<string>)readData;
	//	return worker;
	//}
}
