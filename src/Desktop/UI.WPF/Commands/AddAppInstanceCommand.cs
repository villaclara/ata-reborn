using Application.Director.Instance;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Commands;

public class AddAppInstanceCommand(IDirector director) : CommandBase
{
	private readonly IDirector _director = director;
	public override void Execute(object? parameter)
	{
		_director.AddAppToTrackedList("notepad");
	}

}
