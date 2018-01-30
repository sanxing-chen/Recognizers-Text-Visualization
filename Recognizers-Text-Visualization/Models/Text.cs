using System.ComponentModel.DataAnnotations;

namespace Recognizers_Text_Visualization.Models
{
    public enum Lang
    {
        Chinese,
        English,
        French,
        Spanish,
        Portuguese
    }

    public class Text
    {
        [Key] public int Id { get; set; }
        [Required] public string Content { get; set; }
        public Lang Language { get; set; }
    }
}