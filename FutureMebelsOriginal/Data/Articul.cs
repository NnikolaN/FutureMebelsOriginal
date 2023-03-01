using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutureMebelsOriginal.Data
{
    public class Articul
    {
        public int Id { get; set; }

        public int TypeId { get; set; }
        public Type Types { get; set; }


        public int CategoryId { get; set; }
        public Category Categories { get; set; }


        public string NameModel { get; set; }

      
        public string  Size { get; set; }

        public TypeApplied Applied { get; set; }

        public string ImgUrl { get; set; }


        [Column(TypeName = "decimal(18, 6)")]
        public decimal Price { get; set; }

       

        public DateTime RegisterOn { get; set; }

        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
