using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Models;

namespace UI.WPF.ViewModels;

public class ChangelogPageViewModel : BaseViewModel
{

	public ObservableCollection<SingleVersionChangelogNote> ChangelogNotes { get; set; }

	public ChangelogPageViewModel()
	{
		ChangelogNotes =
		[
			new()
			{
				VersionName = "1.0",
				Notes = new List<string>()
				{
					"changelog1",
					"change2"
				}
			}
		];

		ChangelogNotes.Add(new()
		{
			VersionName = "1.1",
			Notes = new List<string>()
			{
				"1.1_change1"
			}
		});
		
	}

	public string TextName { get; set; } = "BRUH";

}
