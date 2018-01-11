using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vega.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; } 

        public Make()
        {
            Models = new System.Collections.ObjectModel.Collection<Model>();
        }
    }
}