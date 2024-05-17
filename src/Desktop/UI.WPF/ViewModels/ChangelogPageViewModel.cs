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
		
	}

	public string TextName { get; set; } = "BRUH";

}
