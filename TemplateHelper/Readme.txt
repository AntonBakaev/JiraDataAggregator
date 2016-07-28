/*********************************************************************************/
TemplateHelper provides interfaces which modify string templates with runtime data.

ITemplateReplacer:

-----------------------------------------------------------------------------------
//Signature
public interface ITemplateReplacer
{
	string Replace(IReplaceable data, Dictionary<Type, string> allTemplates);
}

where 
	- data implements the empty contract IReplaceable
	- allTemplates is a dictionary of IReplaceable types and string templates.

-----------------------------------------------------------------------------------
//Example
public class IssueVm : IReplaceable
{
	public string Name { get; set; }
	public int Count { get; set; }
}

public class IssueDependencyVm : IReplaceable
{
	public IssueVm Issue { get; set; }
	public List<IssueVm> DepIssueList { get; set; }
}

var issueDepVm = new IssueDependencyVm 
{ 
	Issue = Issue1, 
	DepIssueList = new List<IssueVm> { Issue3, Issue2, Issue4 } 
};
Dictionary<Type, string> allTemplates = new Dictionary<Type, string>
{
	{typeof(IssueVm), @"Description: Issue {Name} Total count {Count} \n"},
	{typeof(IssueDependencyVm), @"Static text \n {Issue} : {DepIssueList}"}
};
ITemplateReplacer replacer = new TemplateReplacer();
string result = replacer.Replace(issueDepVm, allTemplates);

"Static text \n Description: Issue Os1 Total count 3 \n : Description: Issue Os3 Total
count 5 \nDescription: Issue Os2 Total count 1 \nDescription: Issue Os4 Total count 2 \n"
------------------------------------------------------------------------------------
//Rules
- {PropertyName} syntax is used for substitution the property value.
- It's allowed to use only SIMPLETYPEs, STRINGs, MYCLASS and LIST<MYCLASS> for properties types. 
	No other formats including LIST<SIMPLETYPE> or LIST<STRING> are being supported.
	 