using Library.Enums;

namespace LicenseManagerClient.Extensions;

public static class TaskExtensions
{
    public static async Task<(EResult isCanceled, T Result)> SuppressCancellationThrow<T>(this Task<T> task)
    {
        try
        {
            var result = await task;
            var status = task.Status == TaskStatus.Faulted
                ? EResult.Failed
                : EResult.Success;

            return (status, result);
        }
        catch (OperationCanceledException)
        {
            return (EResult.Abort, default!);
        }
    }
}