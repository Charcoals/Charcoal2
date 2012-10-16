using System.ComponentModel.DataAnnotations;
using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class ProjectCreateModel
    {
        [Required]
        [Display(Name = "Project Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Initial Velocity")]
        public int Velocity { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public Project Convert()
        {
            return new Project
                       {
                           Title = Title,
                           Description = Description,
                           Velocity = Velocity
                       };
        }
    }
}