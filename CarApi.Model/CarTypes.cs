using System;
using System.Collections.Generic;

namespace CarApi.Model
{
    public enum CarTypes
    {
        Opel = 1,
        Citroen = 2,
        Nissan = 3,
    }
     
    public enum CarModels
    {
        C5 = 1,
        Astra = 2,
        Meriva = 3,
        Qashqai = 4,
        Antara = 5,
        Yeti = 6,
        Juke = 7,
        P3008 = 8,
        BMW3 = 9,
        P508 = 10,
        VS60 = 11,
        BMW740 = 12
    }
    public static class CarEnumHelper
    {
        public static List<CarModels> GetCarModels(CarTypes carType)
        {
            switch (carType)
            {
                case CarTypes.Citroen:
                    return new List<CarModels>() { CarModels.C5 };
                case CarTypes.Opel:
                    return new List<CarModels>() { CarModels.Astra, CarModels.Meriva};
            }

            return new List<CarModels>();
        }

        public static string GetCarModelId(CarModels carModel)
        {
            switch (carModel)
            {
                case CarModels.Astra:
                    return "%5B60%5D=527";
                case CarModels.Meriva:
                    return "%5B60%5D=519";
                case CarModels.C5:
                    return "%5B92%5D=1184";
                case CarModels.Qashqai:
                    return "%5B62%5D=603";
                case CarModels.Antara:
                    return "%5B60%5D=533";
                case CarModels.Yeti:
                    return "%5B48%5D=16003";
                case CarModels.Juke:
                    return "%5B62%5D=16655";
                case CarModels.P3008:
                    return "%5B59%5D=16187";
                case CarModels.BMW3:
                    return "%5B97%5D=1319";
                case CarModels.P508:
                    return "%5B59%5D=16860";
                case CarModels.VS60:
                    return "%5B42%5D=157";
                case CarModels.BMW740:
                    return "%5B97%5D=10915";
            }

            throw new ArgumentException(nameof(GetCarModelId));
        }
    }
}
