namespace Common.Messages
{
	public static class JiraDataAggregatorExceptionMessages
	{
		public static class RestExceptionMessages
		{
			public const string BadRequestError = "The request could not be understood by the server due to malformed syntax";
			public const string UnauthorizedError = "The request requires user authentication";
			public const string ForbiddenError = "The server understood the request, but is refusing to fulfill it";
			public const string NotFoundError = "The server has not found anything matching the Request-URI";
			public const string MethodNotAllowedError = "The method specified in the Request-Line is not allowed for the resource identified by the Request-URI";
			public const string InternalServerError = "The server encountered an unexpected condition which prevented it from fulfilling the request";
			public const string DefaultError = "Unpredictable rest error occured";
		}

		public static class FileExceptionMessages
		{
			public const string ReadFromFileError = "The file you are trying to access is used by another process";
			public const string WriteToFileError = "Writing data to file is impossible because it is used by another process";
			public const string LoadFromTemplateError = "Failed to load template because it is used by another process";
		}
	}
}
