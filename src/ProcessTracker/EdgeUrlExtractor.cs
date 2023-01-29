using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using FlaUI.UIA3.Patterns;
using static ProcessTracker.ProcessUtils;

namespace ProcessTracker;

public static class EdgeUrlExtractor
{
    public static List<string> Extract()
    {
        var automation = new UIA3Automation();
        var desktop = automation.GetDesktop();
        var windows = desktop.FindAllChildren(c => c.ByControlType(ControlType.Window));
        foreach (var window in windows)
        {
            var title = window.Properties.Name;
            var process = Process.GetProcessById(window.Properties.ProcessId);
            if (process.ProcessName == "msedge")
            {
                var url = GetEdgeUrl(window);
                Console.Out.WriteLine("");
            }
        }
        

        Console.Out.WriteLine("");

    return new List<string>();
    }
    
    public static string GetEdgeUrl(AutomationElement edgeCommandsWindow)
    {
        var addressField = edgeCommandsWindow.FindFirstDescendant(c => c.ByAutomationId("addressEditBox"));
        var textPattern = addressField.Patterns.Text.Pattern;
        return textPattern.DocumentRange.GetText(int.MaxValue);
        
        // var adressEditBox = edgeCommandsWindow.FindFirst(TreeScope.Children,
        //     new PropertyCondition(AutomationElement.AutomationIdProperty, "addressEditBox"));
        //
        // return ((TextPattern)adressEditBox.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(int.MaxValue);
    }
    
}