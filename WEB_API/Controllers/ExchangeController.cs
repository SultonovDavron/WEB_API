using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Model;
using WEB_API.Repository;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        IExchangeRepository _exchangeRepository;

        public ExchangeController(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }


        //Get: api/Exchange
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var exchanges = _exchangeRepository.GetAllBranches();
                return Ok(exchanges);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpGet("{date}")]
        public IActionResult GetFind(string date)
        {
            try
            {
                var exchanges = _exchangeRepository.GetFindBranches(date);
                if(exchanges == null)
                {
                    return NotFound();
                }
                return Ok(exchanges);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [HttpPost]
        public IActionResult PostExchange(List<Exchange> exchange)
        {
            try
            {
                if (exchange == null)
                {
                    return BadRequest();
                }
                
                var exchanges = _exchangeRepository.PostExchangeBranches(exchange);
                
                return Ok(exchanges);
            }
            catch (Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
