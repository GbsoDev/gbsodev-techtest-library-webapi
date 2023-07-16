using System.Runtime.Serialization;
using FluentValidation.Results;

namespace GbsoDev.TechTest.Library.Bll.Exception
{
	internal class ValidationRulesException : System.Exception
	{
		public ValidationRulesException()
		{
		}

		public ValidationRulesException(string? message) : base(message)
		{
		}

		public ValidationRulesException(string? message, System.Exception? innerException) : base(message, innerException)
		{
		}

		public ValidationRulesException(string? message, params ValidationResult[] validations) : base(message)
		{
		}

		protected ValidationRulesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}