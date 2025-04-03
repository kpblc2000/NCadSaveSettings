using NCadSave.Infrastructure.Enums;

namespace NCadSave.Infrastructure
{
    /// <summary>Настройки, относящиеся к хранению файлов в NCad</summary>
    public class SaveSettings : IEquatable<SaveSettings>
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

        public bool Equals(SaveSettings? other)
        {
            if (ReferenceEquals(null, other)) return false; 
            if (ReferenceEquals (this, other)) return true;
            return AutosaveFolder.Equals(other.AutosaveFolder, StringComparison.InvariantCultureIgnoreCase)
                && BackupFolder.Equals(other.BackupFolder, StringComparison.InvariantCultureIgnoreCase)
                && HistoryFolder.Equals(other.HistoryFolder, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (SaveSettings)) return false;
            return Equals((SaveSettings)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = (HistoryFolder != null ? HistoryFolder.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (BackupFolder != null ? BackupFolder.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (AutosaveFolder != null ? AutosaveFolder.GetHashCode() : 0);
            return hashCode;
        }
    }
}
