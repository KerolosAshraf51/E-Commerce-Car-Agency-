namespace CarAgency.Models
{
    public class classOfCar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relations
        public List<cars> cars { get; set; }
    }
}
