using System.Runtime.InteropServices;

namespace ProcessTracker;

public static class WindowUtils
{
    public static nint DesktopWindow => GetDesktopWindow();
    
    [DllImport("user32")]
    private static extern IntPtr GetDesktopWindow();
    
}