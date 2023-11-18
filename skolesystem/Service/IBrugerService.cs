using AutoMapper;
using skolesystem;
using skolesystem.DTOs; 
using skolesystem.Models;

// Service for business logic
public interface IBrugerService
{
    Task<BrugerReadDto> GetBrugerById(int id);
    Task<IEnumerable<BrugerReadDto>> GetAllBrugers();
    Task<IEnumerable<BrugerReadDto>> GetDeletedBrugers();
    Task<BrugerReadDto> AddBruger(BrugerCreateDto brugerDto);
    Task UpdateBruger(int id, BrugerUpdateDto brugerDto);
    Task SoftDeleteBruger(int id);
}

public class BrugerService : IBrugerService
{
    private readonly IBrugerRepository _brugerRepository;
    private readonly IMapper _mapper; 

    public BrugerService(IBrugerRepository brugerRepository, IMapper mapper)
    {
        _brugerRepository = brugerRepository;
        _mapper = mapper;
    }

    public async Task<BrugerReadDto> GetBrugerById(int id)
    {
        var bruger = await _brugerRepository.GetById(id);

        if (bruger == null)
        {
            return null; // or throw an exception or handle accordingly
        }
        var brugerDto = _mapper.Map<BrugerReadDto>(bruger);
        return brugerDto;
    }

    public async Task<IEnumerable<BrugerReadDto>> GetAllBrugers()
    {
        var brugers = await _brugerRepository.GetAll();
        return _mapper.Map<IEnumerable<BrugerReadDto>>(brugers);
    }

    public async Task<IEnumerable<BrugerReadDto>> GetDeletedBrugers()
    {
        var deletedBrugers = await _brugerRepository.GetDeletedBrugers();
        return _mapper.Map<IEnumerable<BrugerReadDto>>(deletedBrugers);
    }

    public async Task<BrugerReadDto> AddBruger(BrugerCreateDto brugerDto)
    {
        var bruger = new Bruger
        {
            user_information_id = brugerDto.user_information_id,
            name = brugerDto.name,
            last_name = brugerDto.last_name,
            phone = brugerDto.phone,
            date_of_birth = brugerDto.date_of_birth,
            address = brugerDto.address,
            is_deleted = brugerDto.is_deleted,
            gender_id = brugerDto.gender_id,
            city_id = brugerDto.city_id,
            user_id = brugerDto.user_id,
        };

        await _brugerRepository.AddBruger(bruger);

        var createdBrugerDto = _mapper.Map<BrugerReadDto>(bruger);

        return createdBrugerDto;
    }

    public async Task UpdateBruger(int id, BrugerUpdateDto brugerDto)
    {
        var existingBruger = await _brugerRepository.GetById(id);

        if (existingBruger == null)
        {
            throw new NotFoundException("Bruger not found");
        }

        // Continue with the update logic
        _mapper.Map(brugerDto, existingBruger);

        await _brugerRepository.UpdateBruger(id, existingBruger);
    }



    public async Task SoftDeleteBruger(int id)
    {
        var existingBruger = await _brugerRepository.GetById(id);

        if (existingBruger == null)
        {
            throw new NotFoundException("Bruger not found");
        }


        await _brugerRepository.SoftDeleteBruger(id);
    }
}
