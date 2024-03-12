using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Utilities;

public static class AppsJsonStringConverter
{
	public static JsonSerializerOptions JsonOptions { get; } = new JsonSerializerOptions() { WriteIndented = true };
	public static string ConvertAppsToJson(List<AppInstance> apps) =>
		JsonSerializer.Serialize<List<AppInstance>>(apps, JsonOptions);

	public static List<AppInstance> ConvertJsonToApps(string json)
	{
		if (string.IsNullOrEmpty(json))
			return [];


		return JsonSerializer.Deserialize<List<AppInstance>>(json, JsonOptions) ?? [];
	}
}
