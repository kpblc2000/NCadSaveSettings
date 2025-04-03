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
        public SaveSettings GetSaveSettings()
        {
            SaveSettings result = new SaveSettings();
            RegistryKey hkcu = Registry.CurrentUser;

            using (RegistryKey hive = hkcu.OpenSubKey(_autosaveFolderHive))
            {
                result.AutosaveFolder = hive.GetValue(_autosaveFolderKey).ToString();
                result.BackupFolder = hive.GetValue(_backupFolderKey).ToString();

                byte[] timeout = (byte[])hive.GetValue(_timeoutKey);
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

                byte[] saveMode = (byte[])hive.GetValue(_incSaveModeKey);
                result.IncSaveMode = (IncrementalSaveMode)saveMode[4];

                byte[] useFormat = (byte[])hive.GetValue(_useSaveFormatKey);
                result.UseDefaultFormat = (UseFormat)useFormat[4];
            }

            return result;
        }

        /// <summary>Сохранение настроек nanoCAD</summary>
        /// <param name="settings"></param>
        public void KeepSaveSettings(SaveSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.AutosaveFolder) && !Directory.Exists(settings.AutosaveFolder))
            {
                Directory.CreateDirectory(settings.AutosaveFolder);
            }
            if (!string.IsNullOrEmpty(settings.BackupFolder) && !Directory.Exists(settings.BackupFolder))
            {
                Directory.CreateDirectory(settings.BackupFolder);
            }
            if (!string.IsNullOrEmpty(settings.HistoryFolder) && !Directory.Exists(settings.HistoryFolder))
            {
                Directory.CreateDirectory(settings.HistoryFolder);
            }
            #region Реестр
            RegistryKey hkcu = Registry.CurrentUser;
            using (RegistryKey hive = hkcu.OpenSubKey(_autosaveFolderHive, true))
            {
                hive.SetValue(_autosaveFolderKey, settings.AutosaveFolder);
                hive.SetValue(_backupFolderKey, settings.BackupFolder);
                byte[] timeout = (byte[])hive.GetValue(_timeoutKey);
                timeout[4] = settings.AutosaveTimeout;

                hive.SetValue(_timeoutKey, timeout);
                byte[] createBak = (byte[])hive.GetValue(_createBakKey);
                createBak[4] = (byte)(settings.CreateBak ? 1 : 0);

                hive.SetValue(_createBakKey, createBak);
                byte[] createOrigBak = (byte[])hive.GetValue(_createOrigBakKey);
                createOrigBak[4] = (byte)(settings.CreateOriginalBak ? 1 : 0);
                hive.SetValue(_createOrigBakKey, createOrigBak);
            }

            using (RegistryKey hive = hkcu.OpenSubKey(_historyFolderHive, true))
            {
                hive.SetValue(_historyFolderKey, settings.HistoryFolder);
            }

            using (RegistryKey hive = hkcu.OpenSubKey(_saveProjectsHive, true))
            {
                byte[] format = (byte[]) hive.GetValue(_defaultFormatKey);
                format[4]=(byte)settings.DefaultFormat;
                hive.SetValue(_defaultFormatKey, format);

                byte[] saveMode = (byte[])hive.GetValue(_incSaveModeKey);
                saveMode[4] = (byte)settings.IncSaveMode;
                hive.SetValue(_incSaveModeKey, saveMode);

                byte[] useFormat = (byte[])hive.GetValue(_useSaveFormatKey);
                useFormat[4] = (byte)settings.UseDefaultFormat;
                hive.SetValue(_useSaveFormatKey, useFormat);
            }
            #endregion
        }

        /// <summary>Рекомендованные значения</summary>
        /// <returns></returns>
        public SaveSettings GetRecommendedSaveSettings()
        {
            return new SaveSettings()
            {
                AutosaveTimeout = 20,
                AutosaveFolder = Path.Combine(_customFolder, "Autosave"),
                BackupFolder = Path.Combine(_customFolder, "Backups"),
                CreateBak = true,
                CreateOriginalBak = false,
                DefaultFormat = DwgFormatForSave.Dwg2013,
                HistoryFolder = Path.Combine("History"),
                IncSaveMode = IncrementalSaveMode.Off,
                UseDefaultFormat = UseFormat.AllDocuments
            };
        }

        /// <summary>Восстановить рекомендованные значения</summary>
        public void ResetToRecommendedSaveSettings()
        {
            SaveSettings settings = GetRecommendedSaveSettings();
            KeepSaveSettings(settings);
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
        private string _customFolder => Path.Combine(Environment.GetEnvironmentVariable("appdata"), "kpblc.NCadAutoSave");
    }
}
