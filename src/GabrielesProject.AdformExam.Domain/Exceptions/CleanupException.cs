using System.Globalization;

namespace GabrielesProject.AdformExam.Domain.Exceptions;

public class CleanupException : Exception
{
    public CleanupException() : base() { }

    public CleanupException(string message) : base(message) { }

    public CleanupException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
