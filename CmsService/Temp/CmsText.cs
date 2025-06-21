namespace CmsService.Models
{
    public class CmsText
    {
        public int Id { get; set; }
        public string Page { get; set; } = string.Empty;       
        public string Key { get; set; } = string.Empty; 
        public string Value { get; set; } = string.Empty;
    }
}
