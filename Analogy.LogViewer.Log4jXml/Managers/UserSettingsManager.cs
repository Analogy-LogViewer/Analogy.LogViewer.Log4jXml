using System;
using System.IO;
using Analogy.LogViewer.Template.Managers;
using Newtonsoft.Json;

namespace Analogy.LogViewer.Log4jXml.Managers
{
    //public class UserSettingsManager
    //{
    //    private static readonly Lazy<UserSettingsManager> _instance =
    //        new Lazy<UserSettingsManager>(() => new UserSettingsManager());
    //    public static UserSettingsManager UserSettings { get; set; } = _instance.Value;
    //    public string AffirmationsFileSetting { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Analogy.LogViewer", "AnalogyLog4jXmlSettings.json");
    //    public Log4jXmlSettings Settings { get; set; }


    //    public UserSettingsManager()
    //    {
    //        if (File.Exists(AffirmationsFileSetting))
    //        {
    //            try
    //            {
    //                var settings = new JsonSerializerSettings
    //                {
    //                    ObjectCreationHandling = ObjectCreationHandling.Replace
    //                };
    //                string data = File.ReadAllText(AffirmationsFileSetting);
    //                Settings = JsonConvert.DeserializeObject<Log4jXmlSettings>(data, settings)!;
    //            }
    //            catch (Exception ex)
    //            {
    //                LogManager.Instance.LogException("Error loading user setting file", ex, "Analogy Serilog Parser");
    //                Settings = new Log4jXmlSettings();

    //            }
    //        }
    //        else
    //        {
    //            Settings = new Log4jXmlSettings();
    //        }

    //    }

    //    public void Save()
    //    {
    //        try
    //        {
    //            File.WriteAllText(AffirmationsFileSetting, JsonConvert.SerializeObject(Settings));
    //        }
    //        catch (Exception e)
    //        {
    //            LogManager.Instance.LogException("Error saving settings: " + e.Message, e, "Analogy Serilog Parser");
    //        }


    //    }
    //}
}