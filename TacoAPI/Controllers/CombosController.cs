using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TacoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string ApiKey)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            List<Combo> result = dbContext.Combos.Include(c => c.Drink).Include(c => c.Taco).ToList();//include() needs using Microsoft.EntityFrameworkCore;

            return Ok(result);
        }
        [HttpGet("{id}")]

        public IActionResult GetById(string ApiKey, int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            Combo result = dbContext.Combos.Include(c => c.Drink).Include(c => c.Taco).FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost()]

        public IActionResult AddCombo(string ApiKey, Combo newCombo)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            newCombo.Id = 0;
            dbContext.Combos.Add(newCombo);
            dbContext.SaveChanges();
            return Created($"api/Combos/{newCombo.Id}", newCombo);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteCombo(string ApiKey, int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            if (dbContext.Combos.Any(c => c.Id == id)) { return NotFound(); }

            Combo result = dbContext.Combos.Find(id);
            dbContext.Combos.Remove(result);
            dbContext.SaveChanges();

            return NoContent();
    }
}
