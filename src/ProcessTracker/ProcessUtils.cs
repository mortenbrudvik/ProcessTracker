using System.Diagnostics;

namespace ProcessTracker;

public static class ProcessUtils
{
    public static IEnumerable<ApplicationProcess> GetProcesses(Predicate<Process> filter)
    {
        var processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            if (filter(process))
                yield return new ApplicationProcess(process.Id, process.ProcessName);
        }
    }
}