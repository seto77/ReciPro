namespace Crystallography;

public static class AssemblyState
{
    public const bool IsDebug =
#if DEBUG
	true;
#else
    false;
#endif
}
