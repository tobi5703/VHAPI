using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VHAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IMG { get; set; }

        public virtual ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
