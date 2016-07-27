using System.Collections.Generic;
using System.Net;
using Common.Exceptions;

namespace Common.Helpers
{
	public class JdaExceptionHelper
	{
		private static Dictionary<HttpStatusCode, string> restExceptionMessages = new Dictionary<HttpStatusCode, string>
		{
			{HttpStatusCode.BadRequest, JiraDataAggregatorExceptionMessages.RestExceptionMessages.BadRequestError},
			{HttpStatusCode.Unauthorized, JiraDataAggregatorExceptionMessages.RestExceptionMessages.UnauthorizedError},
			{HttpStatusCode.Forbidden, JiraDataAggregatorExceptionMessages.RestExceptionMessages.ForbiddenError},
			{HttpStatusCode.NotFound, JiraDataAggregatorExceptionMessages.RestExceptionMessages.NotFoundError},
			{HttpStatusCode.MethodNotAllowed, JiraDataAggregatorExceptionMessages.RestExceptionMessages.MethodNotAllowedError},
			{HttpStatusCode.InternalServerError, JiraDataAggregatorExceptionMessages.RestExceptionMessages.InternalServerError},
		};
		
		public static string GetSpecificRestException(HttpStatusCode statusCode)
		{
			return !restExceptionMessages.ContainsKey(statusCode) 
				? JiraDataAggregatorExceptionMessages.RestExceptionMessages.DefaultError
				: restExceptionMessages[statusCode];
		}
	}
}
