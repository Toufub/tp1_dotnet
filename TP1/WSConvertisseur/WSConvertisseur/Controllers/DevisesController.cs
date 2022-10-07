using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> devises;
        public DevisesController()
        {
            this.devises = new List<Devise>();
            this.devises.Add(new Devise(1, "Dollar", 1.08));
            this.devises.Add(new Devise(2, "Franc Suisse", 1.07));
            this.devises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Http response</returns>
        // GET: api/<DevisesController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Devise>), 202)]
        public IEnumerable<Devise> GetAll()
        {
            return this.devises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name="GetDevise")]
        [ProducesResponseType(typeof(Devise), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            Devise devise = (from d in this.devises where d.Id == id select d).FirstOrDefault();
            if (devise == null)
            {
                return NotFound();
            }
            return Ok(devise);
        }

        /// <summary>
        /// Create a currency
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The devise on JSON</param>
        /// <response code="201">When the currency is created</response>
        /// <response code="400">When data are incorrect</response>
        // POST api/<DevisesController>
        [HttpPost]
        [ProducesResponseType(typeof(Devise), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this.devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Modify a currency
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The devise on JSON</param>
        /// <response code="201">When the currency is modify</response>
        /// <response code="400">When data are incorrect</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Devise), 201)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Remove a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found and delete</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Devise), 200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            Devise devise = (from d in this.devises where d.Id == id select d).FirstOrDefault();
            if (devise == null)
            {
                return NotFound();
            } else
            {
                this.devises.Remove(devise);
            }
            return Ok(devise);
        }
    }
}
