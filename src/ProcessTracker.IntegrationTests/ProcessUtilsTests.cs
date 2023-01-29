namespace ProcessTracker.IntegrationTests;

public class ProcessUtilsTests
{
    [Fact]
    public void GetProcesses_ReturnsAllProcesses()
    {
        var processes = ProcessUtils.GetProcesses(_ => true);
        Assert.NotEmpty(processes);
    }
    
    [Fact]
    public void GetProcesses_ReturnsOnlyNotepadProcesses()
    {
        var processes = ProcessUtils.GetProcesses(p => p.ProcessName == "notepad");
        Assert.All(processes, p => Assert.Equal("notepad", p.Name));
    }
}