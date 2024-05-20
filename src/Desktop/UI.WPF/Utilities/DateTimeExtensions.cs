using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Utilities;

public static class DateTimeExtensions
{
	/// <summary>
	/// Gets the <see cref="IEnumerable{T}"/> of <see cref="DateOnly"/> with Start Date the one calling the method. End Date - Today.
	/// </summary>
	/// <param name="date">Calling object, used as Start Date.</param>
	/// <returns><see cref="IEnumerable{T}"/> of <see cref="DateOnly"/> range.</returns>
	public static IEnumerable<DateOnly> GetDatesOnlyRangeFromDateToToday(this DateTime date)
	{
		var dates = new List<DateOnly>();
		for(DateTime d = date; d <= DateTime.Today.AddDays(1); d = d.AddDays(1))
		{
			dates.Add(DateOnly.FromDateTime(d));
		}

		return dates;
	}
}
