using System.Windows.Controls;
using Application.Director.Instance;

namespace UI.WPF.ViewModels;

public class ContextMenuStripViewModel
{
	private readonly IDirector _director;

	public ContextMenuStrip ContextMenuStrip { get; set; } = new();
	public MenuItem[] Items { get; set; }
	public ContextMenuStripViewModel(IDirector director)
	{
		_director = director;
		//_director.WorkDone -= OnDirectorWorkDone;
		//_director.WorkDone += OnDirectorWorkDone;

		foreach (var app in _director.Apps)
		{
			ContextMenuStrip.Items.Add($"{app.Name} - {app.CurrentSessionTime}");
		}
	}

	private Task OnDirectorWorkDone(object arg1, int arg2)
	{
		throw new NotImplementedException();
	}
}
