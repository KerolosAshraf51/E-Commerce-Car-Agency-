using System.ComponentModel.DataAnnotations.Schema;

namespace CarAgency.Models
{
    public class cars
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public string? ImageURL { get; set; } //nullable temporary
        public int EngineCapacity { get; set; }


        //Relations 

        [ForeignKey("_class")]
        public int classID { get; set; }
        public classOfCar _class { get; set; }  
        public List<review>? reviews {get; set;}

        [ForeignKey("seller")]

        //can't be null (عشان مترفعتش م الهوا اكيد في واحد عرضها للبيع)
        public int sellerID { get; set; }
        public applicationUser seller { get; set; }

        
    }
}
