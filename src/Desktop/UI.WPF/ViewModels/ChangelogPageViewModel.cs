using CommunityToolkit.Mvvm.ComponentModel;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class ChangelogPageViewModel : BaseViewModel
{
	private readonly IChangelogService _changelogService;


	[ObservableProperty]
	private IEnumerable<SingleVersionChangelogNote> _changelogNotes = [];

	//public IEnumerable<SingleVersionChangelogNote> ChangelogNotes { get; } = [];

	public ChangelogPageViewModel(IChangelogService changelogService)
	{

		_changelogService = changelogService;

		ChangelogNotes = _changelogService.GetAllChangelog().Reverse();
	}

	public string TextName { get; set; } = "BRUH";

}
