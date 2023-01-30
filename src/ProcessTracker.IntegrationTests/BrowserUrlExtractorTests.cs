using FluentAssertions;
using Xunit.Abstractions;

namespace ProcessTracker.IntegrationTests;

public class BrowserUrlExtractorTests
{
    private readonly ITestOutputHelper _logger;

    public BrowserUrlExtractorTests(ITestOutputHelper logger)
    {
        _logger = logger;
    }
    
    [Fact]
    public void ExtractAll_ShouldNotBeEmpty()
    {
        var windows = Window.GetApplicationWindows();
        var browser = new BrowserUrlExtractor();
        var urls = browser.Extract(windows).ToList();
        _logger.WriteLine(string.Join("\n", urls));

        urls.Should().NotBeEmpty();
    }
    
    [Fact]
    public void ExtractAll_ShouldNotContainDuplicates()
    {
        var windows = Window.GetApplicationWindows();
        var browser = new BrowserUrlExtractor();
        var urls = browser.Extract(windows);
        
        urls.Should().OnlyHaveUniqueItems();
    }
    
    [Fact]
    public void ExtractAll_ShouldNotContainEmptyStrings()
    {
        var windows = Window.GetApplicationWindows();
        var browser = new BrowserUrlExtractor();
        var urls = browser.Extract(windows);
        
        urls.Should().NotContain(string.Empty);
    }
}