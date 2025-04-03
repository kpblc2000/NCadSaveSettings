using System.ComponentModel;

namespace NCadSave.Infrastructure.Enums
{
    /// <summary>Режим инкрементального сохранения</summary>
    public enum IncrementalSaveMode
    {
        [Description("Отключено")]
        Off = 0,
        [Description("При сохранении")]
        OnSave = 1,
        [Description("При автосохранении и сохранении")]
        OnSaveAndAutoSave = 2,
    }
}
