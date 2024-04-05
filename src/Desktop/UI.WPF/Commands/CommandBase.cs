﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Commands;

public abstract class CommandBase : IRelayCommand
{
	public event EventHandler? CanExecuteChanged;

	public virtual bool CanExecute(object? parameter)
	{
		return true;
	}

	public abstract void Execute(object? parameter);

	public virtual void NotifyCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, new EventArgs());
	}
}