using System.Reflection;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.ViewModels;

public partial class WhatsNewViewModel
{
	public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0";

	private readonly IChangelogService _changelog;

	public WhatsNewViewModel(IChangelogService changelogService)
	{
		_changelog = changelogService;
	}

}
