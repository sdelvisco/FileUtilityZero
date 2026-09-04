using FileUtilityZero.Core;

namespace FileUtilityZero.Core.Tests.TestDoubles;

public sealed class FakeLogger : ILogger
{
    public List<string> Messages { get; } = new();

    public void Log(string message) => Messages.Add(message);
}
