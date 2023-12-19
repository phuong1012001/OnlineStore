namespace OnlineStore.Api.Models.Responses.Base
{
    public class ExecutionRes
    {
        public bool Success { get; set; }

        public ErrorRes? Errors { get; set; }
    }
}