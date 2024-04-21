using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.ViewModels;

namespace UI.WPF.Services.Abstracts;

/// <summary>
/// Interface for navigating between UserControls in Application.
/// </summary>
public interface INavigationService
{
	/// <summary>
	/// Has the current ViewModel object.
	/// </summary>
	BaseViewModel CurrentView { get; }

	/// <summary>
	/// Navigate to desired ViewModel. Where T : BaseViewModel.
	/// </summary>
	/// <typeparam name="T">The type/class name of the ViewModel to Navigate where.</typeparam>
	void NavigateTo<T>() where T : BaseViewModel;
}