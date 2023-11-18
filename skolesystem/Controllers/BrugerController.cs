using Microsoft.AspNetCore.Mvc;
using skolesystem.DTOs;


namespace skolesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController : ControllerBase
    {
        private readonly IBrugerService _brugerService;

        public BrugerController(IBrugerService brugerService)
        {
            _brugerService = brugerService;

        }

        [HttpGet]
        public async Task<IEnumerable<BrugerReadDto>> GetBrugers()
        {
            var brugers = await _brugerService.GetAllBrugers();
            var brugerDtos = brugers.Select(bruger => new BrugerReadDto
            {
                user_information_id = bruger.user_information_id,
                name = bruger.name,
                last_name = bruger.last_name,
                phone = bruger.phone,
                date_of_birth = bruger.date_of_birth,
                address = bruger.address,
                is_deleted = bruger.is_deleted,
                gender_id = bruger.gender_id,
                city_id = bruger.city_id,
                user_id = bruger.user_id
            }).ToList();

            return brugerDtos;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrugerReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBrugerById(int id)
        {
            var bruger = await _brugerService.GetBrugerById(id);

            if (bruger == null)
            {
                return NotFound();
            }

            var brugerDto = new BrugerReadDto
            {
                user_information_id = bruger.user_information_id,
                name = bruger.name,
                last_name = bruger.last_name,
                phone = bruger.phone,
                date_of_birth = bruger.date_of_birth,
                address = bruger.address,
                is_deleted = bruger.is_deleted,
                gender_id = bruger.gender_id,
                city_id = bruger.city_id,
                user_id = bruger.user_id
            };

            return Ok(brugerDto);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateBruger(BrugerCreateDto brugerDto)
        {
            try
            {
                // Your logic to create the Bruger entity and get the created BrugerReadDto
                var createdBrugerDto = await _brugerService.AddBruger(brugerDto);

                // Return the created BrugerReadDto
                return CreatedAtAction(nameof(GetBrugerById), new { id = createdBrugerDto.user_information_id }, createdBrugerDto);
            }
            catch (ArgumentException ex)
            {
                // User with the specified ID already exists
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBruger(int id, BrugerUpdateDto brugerDto)
        {
            try
            {
                var existingBruger = await _brugerService.GetBrugerById(id);

                if (existingBruger == null)
                {
                    throw new NotFoundException("Bruger not found");
                }

                await _brugerService.UpdateBruger(id, brugerDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBruger(int id)
        {
            try
            {
                await _brugerService.SoftDeleteBruger(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
