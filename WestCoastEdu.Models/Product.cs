using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WestCoastEdu.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Length { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [ValidateNever]
        public Status Status { get; set; }
    }
}

