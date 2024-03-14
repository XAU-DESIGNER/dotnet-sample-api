using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class PointOdInerestForUpdate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}