namespace Rubic.AspNetCore.Exceptions;

public class ErrorResult
{
    public string Message { get; set; }
    public string Code { get; set; }
    public IEnumerable<ErrorResultDetail> Details { get; set; }
}

public class RawErrorResult : ErrorResult
{
    public object[] MessageArgs { get; set; }
}
