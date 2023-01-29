namespace ProcessTracker;

public record ApplicationProcess(int Id, string Name, nint MainWindowHandle = default);