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
    }
}
