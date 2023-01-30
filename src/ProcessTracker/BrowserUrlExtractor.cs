using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;

namespace ProcessTracker;

public class BrowserUrlExtractor
{
    private readonly Dictionary<string, Func<AddressField, bool>> _addressFieldMatchers = new(); 

    public BrowserUrlExtractor()
    {
        _addressFieldMatchers.Add("chrome", AddressFieldMatchers.ChromeAddressFieldMatcher);
        _addressFieldMatchers.Add("msedge", AddressFieldMatchers.EdgeAddressFieldMatcher);
        _addressFieldMatchers.Add("brave", AddressFieldMatchers.ChromeAddressFieldMatcher);
    }
    
    public IEnumerable<string> Extract(IEnumerable<Window> windows)
    {
        var automation = new UIA3Automation();
        var desktop = automation.GetDesktop();
        var browserWindows = windows.Where(x => _addressFieldMatchers.ContainsKey(x.ProcessName));
        foreach (var window in browserWindows)
        {
            var addressFieldMatcher = _addressFieldMatchers[window.ProcessName];
            var windowElement = automation.FromHandle(window.Handle);
            var url = GetAddressFieldUrl(windowElement, addressFieldMatcher);
            Debug.WriteLine("url: " + url);
            yield return url;
        }
    }

    private static string GetAddressFieldUrl(AutomationElement window, Func<AddressField, bool> addressFieldMatcher)
    {
        var conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());
        var editFields = window.FindAllDescendants(conditionFactory.ByControlType(ControlType.Edit));

        var addressField = editFields.Where(x => addressFieldMatcher(new AddressField(x))).ToList().FirstOrDefault();
        var url = addressField?.Patterns?.Value?.Pattern?.Value ?? "";
        return url;
    }
}

public class AddressField
{
    private readonly AutomationElement _automationElement;

    public AddressField(AutomationElement automationElement) => _automationElement = automationElement;

    public string AccessKey => _automationElement.Properties.AccessKey;
    public string ClassName => _automationElement.ClassName ?? "";
    public string AutomationId => _automationElement.Properties.AutomationId ?? "";
}

public static class AddressFieldMatchers
{
    public static Func<AddressField, bool> ChromeAddressFieldMatcher => editField => !string.IsNullOrWhiteSpace(editField.AccessKey);
    public static Func<AddressField, bool> EdgeAddressFieldMatcher => editField => editField.ClassName == "OmniboxViewViews" || editField.AutomationId == "view_1026";
}