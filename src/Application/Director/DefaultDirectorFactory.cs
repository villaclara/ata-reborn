using Application.Common.Services;
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
	private static ADirector? _director;
	private static readonly object obj = new();

	public override ADirector CreateDirector()
	{
		if(_director == null)
		{
			lock(obj)
			{
				_director = new DefaultDirector();
			}
		}
		
		return _director;
		
	}
}
