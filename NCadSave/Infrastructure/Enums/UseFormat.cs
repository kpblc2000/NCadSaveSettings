using System.ComponentModel;

namespace NCadSave.Infrastructure.Enums
{
    /// <summary>Использование выбранного формата хранения</summary>
    public enum UseFormat
    {
        [Description("Не использовать")]
        Off = 0,
        [Description("Для новых документов")]
        NewDocuments = 1,
        [Description("Для всех документов")]
        AllDocuments = 3,
    }
}
