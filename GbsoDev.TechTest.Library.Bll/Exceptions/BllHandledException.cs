namespace GbsoDev.TechTest.Library.Bll.Exception
{
	internal sealed class BllHandledException : System.Exception
	{
		public BllHandledException()
		{
		}

		public BllHandledException(string? message) : base(message)
		{
		}

		public BllHandledException(string? message, System.Exception? innerException) : base(message, innerException)
		{
		}
	}
}
