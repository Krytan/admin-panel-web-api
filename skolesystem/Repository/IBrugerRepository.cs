using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.DTOs;
using skolesystem.Models;

// Repository for data access
public interface IBrugerRepository
{
    Task<Bruger> GetById(int id);
    Task<IEnumerable<Bruger>> GetAll();
    Task<IEnumerable<Bruger>> GetDeletedBrugers();
    Task AddBruger(Bruger bruger);
    Task UpdateBruger(int id, Bruger updatedBruger);
    Task SoftDeleteBruger(int id);
}

public class BrugerRepository : IBrugerRepository
{
    private readonly BrugerDbContext _context;

    public BrugerRepository(BrugerDbContext context)
    {
        _context = context;
    }

    public async Task<Bruger> GetById(int id)
    {
        return await _context.Bruger.FindAsync(id);
    }

    public async Task<IEnumerable<Bruger>> GetAll()
    {
        return await _context.Bruger.ToListAsync();
    }

    public async Task<IEnumerable<Bruger>> GetDeletedBrugers()
    {
        return await _context.Bruger.Where(b => b.is_deleted).ToListAsync();
    }

    public async Task AddBruger(Bruger bruger)
    {
        _context.Bruger.Add(bruger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBruger(int id, Bruger updatedBruger)
    {
        var existingBruger = await _context.Bruger.FindAsync(id);

        if (existingBruger != null)
        {
            // Update properties of existingBruger with updatedBruger
            existingBruger.user_information_id = updatedBruger.user_information_id;
            existingBruger.name = updatedBruger.name;
            existingBruger.last_name = updatedBruger.last_name;
            existingBruger.phone = updatedBruger.phone;
            existingBruger.date_of_birth = updatedBruger.date_of_birth;
            existingBruger.address = updatedBruger.address;
            existingBruger.is_deleted = updatedBruger.is_deleted;
            existingBruger.gender_id = updatedBruger.gender_id;
            existingBruger.city_id = updatedBruger.city_id;

            await _context.SaveChangesAsync();
        }
    }

    public async Task SoftDeleteBruger(int id)
    {
        var brugerToDelete = await _context.Bruger.FindAsync(id);

        if (brugerToDelete != null)
        {
            brugerToDelete.is_deleted = true;
            await _context.SaveChangesAsync();
        }
    }
}



