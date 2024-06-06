using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoAPI.Models;

namespace TacoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAllDrinks(string ApiKey, string? SortByCost = null)
        {
            if(!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            List<Drink> result = dbContext.Drinks.ToList();
            
                if (SortByCost.ToLower().Trim() == "ascending")
                {
                    result = result.OrderBy(x => x.Cost).ToList();
                }
                else if (SortByCost.ToLower().Trim() == "descending")
                {
                    result = result.OrderByDescending(x => x.Cost).ToList();
                }
               
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string ApiKey,int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            Drink result = dbContext.Drinks.Find(id);
            if (result == null)
            {
                return NotFound("Drink not found :'(");

            }
            return Ok(result);
        }

        [HttpPost()]

        public IActionResult AddDrink(string ApiKey, [FromBody] Drink newDrink)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();

            return Created($"/api/Drinks/{newDrink.Id}", newDrink);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrinks(string ApiKey, [FromBody] Drink targetDrink, int id)
        {
            if (!UserDAL.ValidateKey(ApiKey)) { return Unauthorized(); }
            if (id != targetDrink.Id) { return BadRequest(); }
            if (dbContext.Drinks.Any(x => x.Id == id)) { return NotFound(); }

            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }
    }

}

