using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHAPI.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ServiceId { get; set; }
        public string IMG { get; set; }

        public virtual Service Service {get; set; }
    }
}
