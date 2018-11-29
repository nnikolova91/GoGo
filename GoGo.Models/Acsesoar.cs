using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoGo.Models
{
    public class Acsesoar
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IHaveThis { get; set; }

        public string DestinationId { get; set; }
        public Destination Destination { get; set; }
    }
}
