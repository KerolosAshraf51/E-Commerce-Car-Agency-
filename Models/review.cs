using System.ComponentModel.DataAnnotations.Schema;

namespace CarAgency.Models
{
    public class review
    {
        public int Id { get; set; }
        public string description { get; set; }

        public DateTime Date { get; set; }

        //Relations

        [ForeignKey("car")]
        public int carID { get; set; }
        public cars car { get; set; }


        [ForeignKey("client")]
        public int clientID { get; set; }
        public applicationUser client { get; set; } 

    }
}
