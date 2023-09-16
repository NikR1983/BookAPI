using Microsoft.AspNetCore.Mvc;
using BookAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        //create a private readonly field of type bookcontext
        private readonly CarContext _context;

        //create a constructor
        public CarController(CarContext context)
        {
            //assign the context to the private field
            _context = context;
        }


        //create all methods to get all the cars
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return all the cars
            return Ok(await _context.Cars.ToListAsync());
        }

        //create a get method to get a car by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //find the car by id
            var car = await _context.Cars.FindAsync(id);

            //if car is null
            if (car == null)
            {
                //return not found
                return NotFound();
            }

            //return the car
            return Ok(car);
        }

        //create method to add a car
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            //add the car to the database
            _context.Cars.Add(car);

            //save the changes
            await _context.SaveChangesAsync();

            //return created
            return CreatedAtAction("Get", new { id = car.Id }, car);
        }

        //create a put method to update a car
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Car car)
        {
            //if id is not equal to car id
            if (id != car.Id)
            {
                //return bad request
                return BadRequest();
            }

            //update the car
            _context.Entry(car).State = EntityState.Modified;

            //save the changes
            await _context.SaveChangesAsync();

            //return no content
            return NoContent();
        }

        //create a delete method to delete a car
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //find the car by id
            var car = await _context.Cars.FindAsync(id);

            //if car is null
            if (car == null)
            {
                //return not found
                return NotFound();
            }

            //remove the car
            _context.Cars.Remove(car);

            //save the changes
            await _context.SaveChangesAsync();

            //return the car
            return Ok(car);
        }


    }
}
