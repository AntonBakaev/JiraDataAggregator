using System;
using System.Collections.Generic;

namespace TemplateHelper
{
	public interface ITemplateReplacer
	{
		string Replace(IReplaceable data, Dictionary<Type, string> allTemplates);
	}
}
