using System;
using System.Collections.Generic;

namespace TemplateReplacer
{
	public interface ITemplateReplacer
	{
		string Replace(IReplaceable data, Dictionary<Type, string> allTemplates);
	}
}
