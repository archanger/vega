using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vega.Models;

namespace vega.Controllers.Resources
{
    public class MakeResource
    {
        public int Id { get; set; }
        // [Required]
        // [StringLength(255)]
        public string Name { get; set; }
        public ICollection<ModelResource> Models { get; set; } 

        public MakeResource()
        {
            Models = new System.Collections.ObjectModel.Collection<ModelResource>();
        }
    }
}