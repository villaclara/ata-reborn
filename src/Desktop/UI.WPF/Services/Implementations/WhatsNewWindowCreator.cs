using UI.WPF.Services.Abstracts;
using UI.WPF.ViewModels;
using UI.WPF.Views;

namespace UI.WPF.Services.Implementations;

public class WhatsNewWindowCreator(WhatsNewViewModel viewModel) : IWindowCreator
{
	/// <inheritdoc/>
	public void CreateWindow()
	{
		var view = new WhatsNewWindow()
		{
			DataContext = viewModel
		};

		view.ShowDialog();
	}
}
