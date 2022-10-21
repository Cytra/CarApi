using System;
using System.Collections.Generic;

namespace CarApi.Model
{
    public enum CarCompany
    {
        Opel = 1,
        Citroen = 92,
        Nissan = 3,
        Peugeot = 4,
        Skoda = 48,
        Toyota = 44,
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
        B740 = 12,
        P2008 = 13,
        Fabia = 14,
        Auris = 15,
        Clio = 16,
    }
    public static class CarEnumHelper
    {
        public static List<CarModels> GetCarModels(CarCompany carCompany)
        {
            switch (carCompany)
            {
                case CarCompany.Citroen:
                    return new List<CarModels>() { CarModels.C5 };
                case CarCompany.Opel:
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
                case CarModels.B740:
                    return "%5B97%5D=10915";
                case CarModels.P2008:
                    return "%5B97%5D=18702";
                case CarModels.Fabia:
                    return "%5B48%5D=339";
                case CarModels.Auris:
                    return "%5B44%5D=279";
                case CarModels.Clio:
                    return "%5B54%5D=418";
            }

            throw new ArgumentException(nameof(GetCarModelId));
        }
    }
}
