using System.IO;

public class SavesConfiguration
{
    public string LocationPath { get; set; }
    public string CurrentSave { get; set; }

    public string CurrentSavePath => Path.Combine(LocationPath, CurrentSave);
}
