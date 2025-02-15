using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public class ChangelogPageViewModel : BaseViewModel
{
	private readonly IChangelogService _changelogService;

	public IEnumerable<SingleVersionChangelogNote> ChangelogNotes { get; } = [];

	public ChangelogPageViewModel(IChangelogService changelogService)
	{

		_changelogService = changelogService;

		ChangelogNotes = _changelogService.GetAllChangelog();
	}

	public string TextName { get; set; } = "BRUH";

}
