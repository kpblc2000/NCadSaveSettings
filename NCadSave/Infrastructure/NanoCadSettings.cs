using Microsoft.Win32;
using NCadSave.Infrastructure.Enums;
using System.IO;

namespace NCadSave.Infrastructure
{
    public class NanoCadSettings
    {
        /// <summary>Настройик NCad.  В текущих реалиях - только касаемо автосохранения</summary>
        /// <param name="registryHiveName">Путь к разделу реестр, в конце обязательно должен включать подкаталог Profiles\ProfileName</param>
        public NanoCadSettings(string registryHiveName)
        {
            RegistryHiveName = registryHiveName;
        }

        /// <summary>Чтение имеющихся настроек nanoCAD из реестра</summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SaveSettings GetSaveSettings()
        {
            SaveSettings result = new SaveSettings();
            RegistryKey hkcu = Registry.CurrentUser;

            using (RegistryKey hive = hkcu.OpenSubKey(_autosaveFolderHive)) {
                result.AutosaveFolder = hive.GetValue(_autosaveFolderKey).ToString();
                result.BackupFolder = hive.GetValue(_backupFolderKey).ToString();

                byte[] timeout = (byte[]) hive.GetValue(_timeoutKey);
                result.AutosaveTimeout = timeout[4];

                byte[] createBak = (byte[])hive.GetValue(_createBakKey);
                result.CreateBak = createBak[4] == 1;

                byte[] createOrigBak = (byte[])hive.GetValue(_createOrigBakKey);
                result.CreateOriginalBak = createOrigBak[4] == 1;
            }

            using (RegistryKey hive = hkcu.OpenSubKey(_historyFolderHive))
            {
                result.HistoryFolder = hive.GetValue(_historyFolderKey).ToString();
            }

            using (RegistryKey hive = hkcu.OpenSubKey(_saveProjectsHive))
            {
                byte[] format = (byte[])hive.GetValue(_defaultFormatKey);
                result.DefaultFormat = (DwgFormatForSave)format[4];

                byte[] saveMode = (byte[]) hive.GetValue(_incSaveModeKey);
                result.IncSaveMode = (IncrementalSaveMode)saveMode[4];

                byte[] useFormat = (byte[])hive.GetValue(_useSaveFormatKey);
                result.UseDefaultFormat = (UseFormat)useFormat[4];
            }

            throw new NotImplementedException();
        }

        /// <summary>Сохранение настроек nanoCAD</summary>
        /// <param name="settings"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void KeepSaveSettings(SaveSettings settings)
        {
            throw new NotImplementedException ();
        }

        /// <summary>Восстановить рекомендованные значения</summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ResetToRecommendedSaveSettings()
        {
            throw new NotImplementedException ();
        }

        public string RegistryHiveName { get; }

        private string _historyFolderHive => Path.Combine(RegistryHiveName, "FileHistory");
        private string _historyFolderKey = "filehistorydir";
        private string _autosaveFolderHive => Path.Combine(RegistryHiveName, "AutoSave");
        private string _autosaveFolderKey => "autodir";
        private string _backupFolderKey => "backupdir";

        private string _saveProjectsHive => Path.Combine(RegistryHiveName, @"IO\SaveProjects");
        private string _defaultFormatKey = "DefaultFormatForSave";
        private string _incSaveModeKey = "IncSaveMode";
        private string _useSaveFormatKey = "UseSaveasFormat";
        private string _timeoutKey = "TimeOut";
        private string _createBakKey = "createback";
        private string _createOrigBakKey = "createfistback";
    }
}
