using Microsoft.AspNetCore.Http;

namespace Rubic.AspNetCore.Exceptions;

public sealed class StatusException : Exception
{
    public StatusCode Status { get; }
    public string Code { get; }
    public object[] MessageArgs { get; }

    public IEnumerable<ErrorResultDetail> Details { get; }

    public StatusException(StatusCode status) : base(string.Empty) => Status = status;

    public StatusException(StatusCode status, string message, params object[] args) : base(message)
    {
        Status = status;
        MessageArgs = args;
    }

    public StatusException(string code, StatusCode status, string message, params object[] args) : this(status, message, args) => Code = code;

    public StatusException(StatusCode status, Exception inner) : this(status, inner.ToString())  {  }

    public StatusException(StatusCode status, IEnumerable<ErrorResultDetail> details, string message, params object[] args) : this(status, message, args)
    {
        Details = details;
    }

    public StatusException(string code, StatusCode status, IEnumerable<ErrorResultDetail> details, string message, params object[] args) : this(code, status, message, args)
    {
        Details = details;
    }
}
