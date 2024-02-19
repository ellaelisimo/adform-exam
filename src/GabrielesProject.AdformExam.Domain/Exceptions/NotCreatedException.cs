using System.Globalization;

namespace GabrielesProject.AdformExam.Domain.Exceptions;

public class NotCreatedException : Exception
{
    public NotCreatedException() : base() { }

    public NotCreatedException(string message) : base(message) { }

    public NotCreatedException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
