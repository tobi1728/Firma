namespace CmsService.Models
{
    public class CmsText
    {
        public int Id { get; set; }
        public string Page { get; set; } = string.Empty;       // np. "Home", "About", "Contact"
        public string Key { get; set; } = string.Empty;        // np. "HeroTitle", "HeroSubtitle"
        public string Value { get; set; } = string.Empty;      // np. "Witamy w naszej stajni!"
    }
}
