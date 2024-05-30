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

	public IList<SingleVersionChangelogNote> ChangelogNotes { get; } = [];

	public ChangelogPageViewModel()
	{
		// Insert New Update Notes here		
		ChangelogNotes.Insert(0, new()
		{
			VersionName = "v1.0",
			Notes = [ "- Initial release." ]
		});

		ChangelogNotes.Insert(0, new()
		{
			VersionName = "v1.1",
			Notes = [
				"- Added ChangeLog Notes.",
				"- Fixed issue with Tracked Apps disappear at random PC crashes."
				]
		});

		ChangelogNotes.Insert(0, new()
		{
			VersionName = "v1.2",
			Notes = [
				"- Bug fixes."
				]
		});

		ChangelogNotes.Insert(0, new()
		{
			VersionName = "v1.3",
			Notes = [
				"- Meet Full History Chart Page! (displaying your activity day by day)",
				"Simply click on 'Three Dots' button next to chart name in App Section."
				]
		});

	}

	public string TextName { get; set; } = "BRUH";

}
