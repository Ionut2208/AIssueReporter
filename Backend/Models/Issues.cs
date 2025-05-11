using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Issues
    {
        [Key]
        public int IdIssue { get; set; }
        public int IdUser { get; set; }
        public string Image {  get; set; }
        public string Found {  get; set; }

    }
}
