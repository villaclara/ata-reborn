using Application.Director.Instance;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WPF.ViewModels;

namespace UI.WPF.Services;


public interface INavigationService
{
	BaseViewModel CurrentView { get; } 
	void NavigateTo<T>() where T : BaseViewModel;
}
public class NavigationService : ObservableObject, INavigationService
{
	private BaseViewModel _currentView;
	private readonly Func<Type, BaseViewModel> _viewModelFactory;

	public BaseViewModel CurrentView
	{
		get => _currentView;
		private set
		{
			_currentView = value;
			OnPropertyChanged();
		}
	}

    public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;

    }

    public void NavigateTo<T>() where T : BaseViewModel
	{

		BaseViewModel model = _viewModelFactory.Invoke(typeof(T));
		CurrentView = model;
	}
}
