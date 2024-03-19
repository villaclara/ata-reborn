﻿using Application.Common.Abstracts;
using Application.Director.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public class DirectorBuilder : IDirectorBuilder
{

	private IDirector _director;
	
	public DirectorBuilder()
	{
		_director = new MainDirector();
		
	}
	public IDirectorBuilder AddReadService(IReadData readData)
	{
		_director.ReadDataService = readData;
		return this;
	}

	public IDirectorBuilder AddWriteService(IWriteData writeData)
	{
		_director.WriteDataService = writeData;
		return this;
	}

	public IDirector Build()
	{
		return _director;
	}

	public IDirectorBuilder SetTimerCheckValue(int timeoutMiliseconds)
	{
		throw new NotImplementedException();
	}

	public IDirectorBuilder SetWhereToWrite(string where)
	{
		throw new NotImplementedException();
	}
}
