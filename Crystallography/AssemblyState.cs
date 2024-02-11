namespace Crystallography;

public static class AssemblyState
{
    public static bool IsDebug =>
#if DEBUG
	true;
#else
    false;
#endif
}
