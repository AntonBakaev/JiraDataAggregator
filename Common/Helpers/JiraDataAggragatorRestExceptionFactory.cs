using System;
using System.Net;
using Common.Exceptions;

namespace Common.Helpers
{
	public static class JiraDataAggragatorRestExceptionFactory
	{
		public static JiraDataAggregatorException GetSpecificRestException(HttpStatusCode statusCode, string absoluteUri)
		{
			string exceptionString;

			switch (statusCode)
			{
				case HttpStatusCode.BadRequest:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.BadRequestError;
					break;

				case  HttpStatusCode.Unauthorized:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.UnauthorizedError;
					break;

				case HttpStatusCode.Forbidden:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.ForbiddenError;
					break;

				case HttpStatusCode.NotFound:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.NotFoundError;
					break;

				case HttpStatusCode.MethodNotAllowed:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.MethodNotAllowedError;
					break;

				case HttpStatusCode.InternalServerError:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.InternalServerError;
					break;

				default:
					exceptionString = JiraDataAggregatorExceptionKeys.RestExceptionKeys.DefaultError;
					break;
			}

			return new JiraDataAggregatorException(exceptionString + " at " + absoluteUri);
		}
	}
}
