using Onebrb.Application.Enums;

namespace Onebrb.Application;

public class HandlerResponse
{
    public HandlerStatus Status { get; set; } = HandlerStatus.Failure;

    public string? Message { get; set; }
}

public class HandlerResponse<TResult> : HandlerResponse where TResult : class
{
    public TResult? Result { get; set; }
}
