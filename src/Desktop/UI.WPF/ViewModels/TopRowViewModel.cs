using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace UI.WPF.ViewModels;

public partial class TopRowViewModel : BaseViewModel
{

	[RelayCommand]
	public void CloseWindow()
	{
		Log.Information("{@Method} - Closing application.", nameof(CloseWindow));
		System.Windows.Application.Current.Shutdown();
	}

	[RelayCommand]
	public void MinimizeWindow()
	{
		System.Windows.Application.Current.MainWindow.Hide();
		System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
	}
}
