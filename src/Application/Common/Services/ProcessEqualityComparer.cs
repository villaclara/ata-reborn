using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace UI.WPF.Services.Implementations;

public class ProcessEqualityComparer : IEqualityComparer<Process>
{
	public bool Equals(Process? x, Process? y)
	{
		if (x is not null && y is not null)
		{
			return x.ProcessName == y.ProcessName;
		}

		return false;
	}

	public int GetHashCode([DisallowNull] Process obj)
	{
		return obj.ProcessName.Length;
	}
}
