﻿using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interactors;

public interface IInteractor
{
	AppInstance GetAppInstace();
}