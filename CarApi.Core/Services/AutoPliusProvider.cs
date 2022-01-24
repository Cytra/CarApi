using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApi.Core.Services
{
    public interface IAutoPliusProvider
    {
        Task Test();
    }
    public class AutoPliusProvider : IAutoPliusProvider
    {
        private readonly IAutoPliusService _autoPliusService;
        public AutoPliusProvider(IAutoPliusService autoPliusService)
        {
            _autoPliusService = autoPliusService;
        }

        
        public async Task Test()
        {
            var page = 1;
            var carListHtml = await _autoPliusService.GetNewAdListPage(page);
        }
    }
}
