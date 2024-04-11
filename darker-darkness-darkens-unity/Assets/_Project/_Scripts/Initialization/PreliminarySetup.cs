using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class PreliminarySetup
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
#else
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
#endif
    public static async void Setup()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && SceneManager.GetActiveScene().buildIndex is not 0)
        {
            await SceneManager.LoadSceneAsync(0);
            ClearConsole();
        }
#endif
        var setups = GetAllPreliminarySetups().OrderBy(setup => setup.Order);
        foreach (var setup in setups)
            setup.Setup();
    }

    private static IEnumerable<IPreliminarySetup> GetAllPreliminarySetups()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        foreach (Type type in types)
            if (typeof(IPreliminarySetup).IsAssignableFrom(type) && !type.IsInterface)
                yield return Activator.CreateInstance(type) as IPreliminarySetup;
    }

#if UNITY_EDITOR
    private static void ClearConsole()
    {
        var entries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var method = entries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        method.Invoke(null, null);
    }
#endif
}