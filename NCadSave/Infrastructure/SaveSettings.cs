using NCadSave.Infrastructure.Enums;

namespace NCadSave.Infrastructure
{
    /// <summary>Настройки, относящиеся к хранению файлов в NCad</summary>
    public class SaveSettings
    {
        /// <summary>Режим инкрементального сохранения</summary>
        public IncrementalSaveMode IncSaveMode { get; set; }
        /// <summary>Формат по умолчанию для сохранения файла</summary>
        public DwgFormatForSave DefaultFormat { get; set; }
        /// <summary>Применять выбранныйй формат <see cref="DefaultFormat"/> к документам...</summary>
        public UseFormat UseDefaultFormat { get; set; }
        /// <summary>Каталог автосохранения</summary>
        public string AutosaveFolder { get; set; }
        /// <summary>Каталог резервных копий</summary>
        public string BackupFolder { get; set; }
        /// <summary>Каталог с файлами историй</summary>
        public string HistoryFolder { get; set; }
        /// <summary>Время автосохранения</summary>
        public byte AutosaveTimeout { get; set; }
        /// <summary>Создавать bak</summary>
        public bool CreateBak { get; set; }
        /// <summary>Создавать bak оригинала</summary>
        public bool CreateOriginalBak { get; set; }
    }
}
