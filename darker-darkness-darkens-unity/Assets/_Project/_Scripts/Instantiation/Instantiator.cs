using UnityEngine;

namespace Euphelia.Instantiation
{
	public static class Instantiator
	{
		public static T InstantiateAndDontDestroy<T>(T prefab) where T : Object
		{
			var instantiated = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instantiated);
			return instantiated;
		}
	}
}