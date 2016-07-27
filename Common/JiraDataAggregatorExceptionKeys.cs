using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public static class JiraDataAggregatorExceptionKeys
	{


		public static class RestExceptionKeys
		{
			public const string BadRequestError = "The request could not be understood by the server due to malformed syntax.";
			public const string UnauthorizedError = "The request requires user authentication.";
			public const string ForbiddenError = "The server understood the request, but is refusing to fulfill it.";
			public const string NotFoundError = "The server has not found anything matching the Request-URI.";
			public const string MethodNotAllowedError = "The method specified in the Request-Line is not allowed for the resource identified by the Request-URI.";
			public const string InternalServerError = "The server encountered an unexpected condition which prevented it from fulfilling the request.";
			public const string DefaultError = "Unpredictable rest error occured.";
		}
	}
}
