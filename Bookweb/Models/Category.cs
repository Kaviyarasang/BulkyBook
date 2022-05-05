using System.ComponentModel.DataAnnotations;

namespace Bookweb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        public string Displayorder { get; set; }
        public DateTime Createdatetime { get; set; } = DateTime.Now;
            
    }
}
