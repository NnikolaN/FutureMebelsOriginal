
using System;
using System.Collections.Generic;

namespace FutureMebelsOriginal.Data {

    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Articul> Articuls { get; set; }
    }
}
