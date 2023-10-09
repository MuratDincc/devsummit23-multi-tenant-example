namespace Rubic.AspNetCore.Exceptions;

public class ErrorResultDetail
{
    public ErrorResultDetail()
    {
        Message = new List<string>();
    }

    public string Field { get; set; }
    public string Code { get; set; }
    public IEnumerable<string> Message { get; set; }
}
