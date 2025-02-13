using System.Reflection;

namespace UI.WPF.ViewModels;

public partial class WhatsNewViewModel : BaseViewModel
{
	public string CurrentVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0";
	public WhatsNewViewModel()
	{

	}

}
