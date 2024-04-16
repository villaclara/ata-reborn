using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.ViewModels;

public partial class TopRowViewModel : BaseViewModel
{

	public TopRowViewModel()
	{

	}

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
