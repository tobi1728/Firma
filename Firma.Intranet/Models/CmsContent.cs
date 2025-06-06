using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models
{
    [Table("CmsEntries")] 
    public class CmsContent
    {
        public int Id { get; set; }
        public string Page { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }


}
