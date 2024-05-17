using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Models;

public class SingleVersionChangelogNote
{
	/// <summary>
	/// Represents Version Name of the patch.
	/// Format - v1.0
	/// </summary>
	public string VersionName { get; set; } = null!;

	/// <summary>
	/// Represents Patch notes.
	/// Just simple list of strings.
	/// </summary>
	public List<string> Notes { get; set; } = null!;
}
