using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarApi.Model;
using CarApi.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CarApi.Samples
{
    public class GetAllAutoPliusCarAddRequestSample : IMultipleExamplesProvider<GetAllAutoPliusCarAddRequest>
    {
        public IEnumerable<SwaggerExample<GetAllAutoPliusCarAddRequest>> GetExamples()
        {
            yield return SwaggerExample.Create("C5", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Citroen,
                CarModel = CarModels.C5,
                YearFrom = 2009,
                YearTo = 2020
            });

            yield return SwaggerExample.Create("Astra", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Opel,
                CarModel = CarModels.Astra,
                YearFrom = 2009,
                YearTo = 2020
            });

            yield return SwaggerExample.Create("Qashqai", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Opel,
                CarModel = CarModels.Antara,
                YearFrom = 2008,
                YearTo = 2012
            });

            yield return SwaggerExample.Create("2008", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Peugeot,
                CarModel = CarModels.P2008,
                YearFrom = 2015,
                YearTo = 2021
            });

            yield return SwaggerExample.Create("Fabia", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Skoda,
                CarModel = CarModels.Fabia,
                YearFrom = 2011,
                YearTo = 2021
            });

            yield return SwaggerExample.Create("Auris", new GetAllAutoPliusCarAddRequest()
            {
                //Company = CarCompany.Toyota,
                CarModel = CarModels.Auris,
                YearFrom = 2011,
                YearTo = 2021
            });
        }
    }
}
