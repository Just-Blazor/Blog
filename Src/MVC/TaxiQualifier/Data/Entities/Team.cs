using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiQualifier.Data.Entities
{
    public class Team
    {
        public int Id { get; set; }

        [StringLength(6, MinimumLength =6)]
        [Required]
        public string Plaque { get; set; }
    }
}
