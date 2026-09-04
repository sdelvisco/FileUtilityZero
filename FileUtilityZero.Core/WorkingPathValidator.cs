namespace FileUtilityZero.Core;

// Pure, stateless validation rule pulled out of the old BtnRun_Click handler.
// Not converted to an instance/injectable class like the rest of Stage 3,
// since it has no dependencies or state to inject - just a plain string rule.
public static class WorkingPathValidator
{
    // True if the path is UNC syntax (\\host\share). Scanning one causes Windows
    // to attempt SMB authentication against that host automatically, which a
    // rogue SMB listener could capture - see the previous fix pass for detail.
    public static bool IsUncPath(string path)
    {
        return path.TrimStart().StartsWith(@"\\", StringComparison.Ordinal);
    }
}
