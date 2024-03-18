using Application.Director.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director.Creation;

public abstract class ADirectorFactory
{
	public abstract ADirector CreateDirector();
}
