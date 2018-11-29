using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models.Photos
{
    public class DestinationPhoto 
    {
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string DestinationId { get; set; }
        public Destination Destination { get; set; }

        public string UserId { get; set; }
        public GoUser User { get; set; }
    }
}
