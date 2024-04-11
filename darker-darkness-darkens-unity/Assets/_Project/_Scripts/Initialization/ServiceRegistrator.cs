using SwiftLocator.Services.ServiceLocatorServices;
using System.IO;
using UnityEngine;

public class ServiceRegistrator : IPreliminarySetup
{
    public int Order => IPreliminarySetup.REGISTER;

    public void Setup()
    {
        SetupDb();
    }

    private void SetupDb()
    {
        ServiceLocator.SingletonRegistrator
            .Register(new SavesConfiguration()
            {
                CurrentSave = "1.db",
                LocationPath = Path.Combine(Application.persistentDataPath, "_Saves")
            })
            .Register<IDatabaseService, DatabaseService>();
    }
}