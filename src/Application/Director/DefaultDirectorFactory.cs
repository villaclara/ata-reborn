using Application.Director.Creation;
using Application.Director.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Director;

public class DefaultDirectorFactory : ADirectorFactory
{
	public override ADirector CreateDirector()
	{
		return new DefaultDirector()
		{
			
		}
	}
}
