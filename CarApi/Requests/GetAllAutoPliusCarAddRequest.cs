using CarApi.Model;

namespace CarApi.Requests
{
    public class GetAllAutoPliusCarAddRequest
    {
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public CarModels CarModel { get; set; }
    }
}
