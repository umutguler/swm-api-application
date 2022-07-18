using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swm.Api.Business.Entities
{
#nullable disable
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string First { get; set; }

        [Required]
        [MinLength(1)]
        public string Last { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }
    }
#nullable enable
}
