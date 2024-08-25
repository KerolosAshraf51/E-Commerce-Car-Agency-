using System.ComponentModel.DataAnnotations.Schema;

namespace CarAgency.Models
{
    public class purchase
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public float price { get; set; }


        [ForeignKey("Client")]
        public int? ClientID { get; set; }
        public applicationUser? Client { get; set; }

        [ForeignKey("Car")]
        public int? CarID { get; set; }
        public cars? Car { get; set; }



        // public Admin? Admin { get; set; }
    }
}
