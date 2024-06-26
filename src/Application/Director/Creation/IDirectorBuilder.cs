﻿using Application.Common.Abstracts;
using Application.Director.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public interface IDirectorBuilder
{
	IDirector Build();

	IDirectorBuilder AddIOServices(IReadData readService, IWriteData writeService);
	IDirectorBuilder SetWritableFile(string where);
	IDirectorBuilder SetTimerCheckValue(int timeoutMiliseconds);

}
