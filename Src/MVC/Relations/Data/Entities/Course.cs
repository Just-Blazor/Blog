using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relations.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public int TournamentId { get; set; }

        public University University { get; set; }
    }
}
