namespace Cmt.Bll.Services.Exceptions
{
    public class ErrorResult
    {
        public string Code { get; set; }

        public ErrorResult(string code)
        {
            Code = code;
        }
    }
}
