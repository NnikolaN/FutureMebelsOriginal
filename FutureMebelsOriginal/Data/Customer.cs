using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data;

namespace FutureMebelsOriginal.Data
{


    public class Customer:IdentityUser
    {
       public string FirstMidName { get; set; }
       public string Adress { get; set; }      
        public ICollection<Order> Orders { set; get; }     
        
    }
}
