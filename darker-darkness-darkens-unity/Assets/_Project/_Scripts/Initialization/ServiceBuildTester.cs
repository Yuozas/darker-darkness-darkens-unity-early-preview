using SwiftLocator.Services.ServiceLocatorServices;

namespace Euphelia.Initialization
{
	public class ServiceBuildTester : IPreliminarySetup
	{
		public int Order => IPreliminarySetup.TEST;

		public void Setup() => ServiceLocator.Build();
	}
}