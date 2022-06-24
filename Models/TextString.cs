using Microsoft.EntityFrameworkCore;

namespace OSDashboardBA.Models
{
    public class TextString
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class TextDTOString
    {
        public string Text { get; set; }
    }
}
