using System.Collections.Generic;

namespace FutureMebelsOriginal.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        ICollection<Articul> Articuls { get; set; }

    }
}
