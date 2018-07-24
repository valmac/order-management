namespace Teraque
{

	/// <summary>
	/// This exception is thrown when a user cancels a critical operations, such as login information.
	/// </summary>
	/// <copyright>Copyright � 2010 - 2012 - Teraque, Inc.  All Rights Reserved.</copyright>
	public class UserAbortException : System.Exception
	{

		/// <summary>
		/// Creates the exception.
		/// </summary>
		/// <param name="message"></param>
		public UserAbortException(string message) : base(message) {}

	}

}