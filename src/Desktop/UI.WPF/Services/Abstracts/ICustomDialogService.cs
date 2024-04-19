using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.Enums;

namespace UI.WPF.Services.Abstracts;

/// <summary>
/// Interface to show Custom Dialog Windows. Primary usage is to show confirmation window when deleting Tracked Application.
/// </summary>
public interface ICustomDialogService
{
	/// <summary>
	/// Show Dialog with Yes/No buttons.
	/// </summary>
	/// <param name="title">Title of the window.</param>
	/// <param name="message">Message displaying.</param>
	/// <returns><see cref="CustomDialogResult"/> based on user choice.</returns>
	CustomDialogResult ShowYesNoDialog(string title, string message);
}
