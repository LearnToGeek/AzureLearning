using CodeCamp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;


namespace CodeCamp.Data
{
    public class CampRepository : ICampRepository
    {
        private readonly ILogger<CampRepository> _logger;


        Camp[] camps =  {
                new Camp {CampId=1,Name="Geek Tech",Code="GT2020",Length=2},
                new Camp {CampId=2,Name="Hackathon",Code="HK2020",Length=2,Location=new Location(){Country="India" } },
                new Camp {CampId=3,Name="Cyber Monday",Code="CM2020",Length=2}
            };

        public CampRepository(ILogger<CampRepository> logger)
        {
            _logger = logger;
        }

        public async Task<Camp[]> GetAllCampsAsync(bool includeTalks = false)
        {
            _logger.LogInformation($"Getting all Camps");

            return await Task.FromResult(camps);
        }

        public async Task<Camp> GetCampAsync(string code, bool includeTalks = false)
        {
            _logger.LogInformation($"Getting a Camp for {code}");

            IQueryable<Camp> query = camps.ToList().AsQueryable().Where(e => e.Code == code);

            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task<Camp[]> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            _logger.LogInformation($"Getting all Camps");

            IQueryable<Camp> query = camps.ToList().AsQueryable().Where(e => e.EventDate == dateTime);

            return await Task.FromResult(query.ToArray());
        }
    }
}
