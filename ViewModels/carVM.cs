using CarAgency.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarAgency.ViewModels
{
    public class carVM
    {
        [DisplayName("Brand")]
        public string Model { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Year { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }

        [DisplayName("Choose image")]
        public string? ImageURL { get; set; } //nullable temporary

        [DisplayName("Engine Size")]
        public int EngineCapacity { get; set; }

        
        [DisplayName("Category")]
        public int classID { get; set; }
        

    }
}
