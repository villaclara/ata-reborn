using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace UI.WPF.Services.Implementations;

public class ProcessEqualityComparer : IEqualityComparer<Process>
{
	public bool Equals(Process? x, Process? y)
	{
		if (ReferenceEquals(x, y))
		{
			return true;
		}

		if (x is null || y is null)
		{
			return false;
		}

		return x.ProcessName == y.ProcessName;
	}

	public int GetHashCode([DisallowNull] Process obj)
	{
		return obj.ProcessName.Length;
	}
}
