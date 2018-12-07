using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int CollectedPoints { get; set; }

        public ICollection<GoUser> Participants { get; set; }
    }
}
