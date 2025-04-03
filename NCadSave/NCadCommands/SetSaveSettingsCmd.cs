using HostMgd.ApplicationServices;
using NCadSave.Infrastructure;
using System.IO;
using System.Windows;
using Teigha.Runtime;
using Application = HostMgd.ApplicationServices.Application;
using MessageBox = System.Windows.MessageBox;

namespace NCadSave.NCadCommands
{
    public class SetSaveSettingsCmd
    {
        [CommandMethod("kpblc-set-savesettings")]
        public static void SetSaveSettingsCommand()
        {
            string regName = Path.Combine(Teigha.DatabaseServices.HostApplicationServices.Current.UserRegistryProductRootKey,
                "Profiles", Application.GetSystemVariable("cprofile").ToString());
            NanoCadSettings settings = new NanoCadSettings(regName);
            SaveSettings existData = settings.GetSaveSettings();
            settings.ResetToRecommendedSaveSettings();
            if (existData != settings.GetRecommendedSaveSettings())
            {
                string message = "Были изменены каталоги в настройках. Настоятельно рекомендуется перезапустить nanoCAD!";

                MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Hand);

                Document doc = Application.DocumentManager.MdiActiveDocument;
                if (doc != null)
                {
                    doc.Editor.WriteMessage($"\n{message}");
                }
            }
        }
    }
}
