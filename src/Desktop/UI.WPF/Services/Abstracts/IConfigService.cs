using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.Services.Abstracts;

public interface IConfigService
{

	string? GetValueFromSection(string sectionName);
	bool WriteSectionWithValues(string sectionName, string value);
}
