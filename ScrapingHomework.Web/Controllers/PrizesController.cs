using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrapingHomework.Data;

namespace ScrapingHomework.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizesController : ControllerBase
    {
        [HttpGet]
        [Route("getprizes")]
        public List<Prize> GetPrizes()
        {
            var scraper = new PrizeScraper();
            return scraper.Scrape();
        }
    }
}
