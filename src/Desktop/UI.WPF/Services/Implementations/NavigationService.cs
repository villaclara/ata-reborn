using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.ViewModels;
using UI.WPF.Services.Abstracts;

namespace UI.WPF.Services.Implementations;

public class NavigationService(Func<Type, BaseViewModel> viewModelFactory, TrackedAppsViewModel trackedAppsViewModel) : ObservableObject, INavigationService
{
    // trackedAppsViewModel - is the default model with TrackedApps, sent to the navigator. To be displayed at start.
    private BaseViewModel _currentView = trackedAppsViewModel;
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
