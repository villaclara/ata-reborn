using CommunityToolkit.Mvvm.ComponentModel;
using UI.WPF.Services.Abstracts;
using UI.WPF.ViewModels;

namespace UI.WPF.Services.Implementations;

public class NavigationService(Func<Type, BaseViewModel> viewModelFactory, IConfigService config,
	TrackedAppsViewModel trackedAppsViewModel,
	TrackedAppsViewModel_Minimal trackedAppsViewModel_Minimal) : ObservableObject, INavigationService
{
	// trackedAppsViewModel - is the default model with TrackedApps, sent to the navigator. To be displayed at start.
	private BaseViewModel _currentView = config.GetBooleanValue("MinimalDashboard") ? trackedAppsViewModel_Minimal : trackedAppsViewModel;
	private readonly Func<Type, BaseViewModel> _viewModelFactory = viewModelFactory;

	public BaseViewModel CurrentView
	{
		get => _currentView;
		private set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}

	public void NavigateTo<T>() where T : BaseViewModel
	{

		BaseViewModel model = _viewModelFactory.Invoke(typeof(T));
		CurrentView = model;
	}
}
