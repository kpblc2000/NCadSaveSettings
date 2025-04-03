using System.ComponentModel;

namespace NCadSave.Infrastructure.Enums
{
    /// <summary>Формат для сохранения файла</summary>
    public enum DwgFormatForSave
    {
        [Description("Dwg 2018")]
        Dwg2018 = 1,
        [Description("Dwg 2013")]
        Dwg2013 = 2,
        [Description("Dwg 2010")]
        Dwg2010 = 3,
        [Description("Dwg 2007")]
        Dwg2007 = 4,
        [Description("Dwg 2004")]
        Dwg2005,
        [Description("Dwg 2000")]
        Dwg2000 = 6,
    }
}
