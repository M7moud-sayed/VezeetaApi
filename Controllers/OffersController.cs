using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testV.Models;

namespace testV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        VezeetaContext db;
        public OffersController(VezeetaContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOffers()
        {
            var offers = await db.GetAllOffersAsync();

            if (offers == null || !offers.Any())
                return NotFound("No offers found.");

            return Ok(offers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var offer = await db.GetOfferByIdAsync(id);

            if (offer == null)
                return NotFound($"Offer with ID {id} not found.");

            return Ok(offer);
        }








    }
}
