using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.WPF.Enums;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.Services.Implementations;


public class CustomDialogService : ICustomDialogService
{
	public CustomDialogResult ShowYesNoDialog(string title, string message)
	{
		var customDialog = MessageBox.Show(message, title, MessageBoxButton.YesNo);

		var result = customDialog == MessageBoxResult.Yes ? CustomDialogResult.Yes : CustomDialogResult.No;
		Log.Information("{@Method} - User chosen - {@result}.", nameof(ShowYesNoDialog), result);
		return result;
	}
}
