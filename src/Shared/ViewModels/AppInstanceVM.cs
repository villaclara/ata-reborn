using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels;

public record class AppInstanceVM
{
	public string Name { get; set; } = null!;
	public bool IsRunning {  get; set; }
	public DateTime LastRunningDate {  get; set; }
	public int CurrentSessionTime {  get; set; }
	public List<UpTime> UpTimeList { get; set; } = new List<UpTime>();
}
