using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoAPI.Models;

namespace TacoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string ApiKey, bool? SoftShell = null)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            List<Taco> result = dbContext.Tacos.ToList();
            if (SoftShell != null)
            {
                result = result.Where(x => x.SoftShell == SoftShell).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(string ApiKey, int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            {
                return NotFound("Taco not found :'(");
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddTaco(string ApiKey, [FromBody] Taco newTaco)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();

            return Created($"/api/Tacos/{newTaco.Id}", newTaco);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteById(string ApiKey, int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            Taco taco = dbContext.Tacos.Find(id);
            if (taco == null) { return NotFound(); }
            dbContext.Tacos.Remove(taco);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}