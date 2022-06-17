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
                CarModel = CarModels.C5,
                YearFrom = 2009,
                YearTo = 2020
            });

            yield return SwaggerExample.Create("Astra", new GetAllAutoPliusCarAddRequest()
            {
                CarModel = CarModels.Astra,
                YearFrom = 2009,
                YearTo = 2020
            });

            yield return SwaggerExample.Create("Qashqai", new GetAllAutoPliusCarAddRequest()
            {
                CarModel = CarModels.Antara,
                YearFrom = 2008,
                YearTo = 2012
            });
        }
    }
}
