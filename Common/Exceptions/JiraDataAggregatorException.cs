using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
	public class JiraDataAggregatorException : Exception
	{
		public JiraDataAggregatorException()
		{
		}

		public JiraDataAggregatorException(string message)
			: base(message)
		{
		}

		public JiraDataAggregatorException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public JiraDataAggregatorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
