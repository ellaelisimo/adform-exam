using System.Globalization;

namespace GabrielesProject.AdformExam.Domain.Exceptions;

public class StatusException : Exception
{
    public StatusException() : base() { }

    public StatusException(string message) : base(message) { }

    public StatusException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}

