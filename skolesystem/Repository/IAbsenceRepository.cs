﻿using Microsoft.EntityFrameworkCore;
using skolesystem.Data;
using skolesystem.Models;

namespace skolesystem.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace skolesystem.Repository
    {
        // Repository for data access
        public interface IAbsenceRepository
        {
            Task<Absence> GetById(int id);
            Task<IEnumerable<Absence>> GetAll();
            Task<IEnumerable<Absence>> GetDeletedAbsences();
            Task AddAbsence(Absence absence);
            Task UpdateAbsence(int id, Absence absence);
            Task SoftDeleteAbsence(int id);
        }

        public class AbsenceRepository : IAbsenceRepository
        {
            private readonly AbsenceDbContext _context;

            public AbsenceRepository(AbsenceDbContext context)
            {
                _context = context;
            }

            public async Task<Absence> GetById(int id)
            {
                return await _context.Absences.FindAsync(id);
            }

            public async Task<IEnumerable<Absence>> GetAll()
            {
                return await _context.Absences.ToListAsync();
            }

            public async Task<IEnumerable<Absence>> GetDeletedAbsences()
            {
                return await _context.Absences.Where(a => a.is_deleted).ToListAsync();
            }

            public async Task AddAbsence(Absence absence)
            {
                _context.Absences.Add(absence);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAbsence(int id, Absence absence)
            {
                var existingAbsence = await _context.Absences.FindAsync(id);

                if (existingAbsence == null)
                {
                    throw new ArgumentException("Absence not found");
                }

                // Map properties from Absence to the entity
                existingAbsence.user_id = absence.user_id;
                existingAbsence.teacher_id = absence.teacher_id;
                existingAbsence.class_id = absence.class_id;
                existingAbsence.absence_date = absence.absence_date;
                existingAbsence.reason = absence.reason;

                await _context.SaveChangesAsync();
            }

            public async Task SoftDeleteAbsence(int id)
            {
                var absenceToDelete = await _context.Absences.FindAsync(id);

                if (absenceToDelete != null)
                {
                    absenceToDelete.is_deleted = true;
                    _context.Entry(absenceToDelete).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

}