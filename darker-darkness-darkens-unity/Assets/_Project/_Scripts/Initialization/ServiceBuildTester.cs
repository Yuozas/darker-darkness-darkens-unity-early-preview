using SwiftLocator.Services.ServiceLocatorServices;

public class ServiceBuildTester : IPreliminarySetup
{
    public int Order => IPreliminarySetup.TEST;

    public void Setup()
    {
        ServiceLocator.Build();
    }
}