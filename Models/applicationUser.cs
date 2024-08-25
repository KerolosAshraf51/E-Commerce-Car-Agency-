using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarAgency.Models

{
    public class applicationUser : IdentityUser<int>
    {
        //all properties needed were inherted from parent class(IdentityUser)
   

        //Relations

/*        [ForeignKey("cars")]
        public int? carID { get; set; }*/
        public List<cars> cars { get; set; }


     /*   [ForeignKey("reviews")]
        public int? reviewID { get; set; }*/
        List<review> reviews { get; set; }
    }
}
