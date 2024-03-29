﻿using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Data.Repositories
{
    public class ExamStemRepository 
    {
        private readonly WebAppDbContext _db;
        public ExamStemRepository(WebAppDbContext context)
        {
            _db = context;
        }
        public async Task<ExamStem?> GetAsync(int id)
        {
            return await _db.ExamStems.AsNoTracking().Include(exs => exs.Exam)
                .Include(exs => exs.Stem).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<ExamStem>?> GetAllAsync()
        {
            return await _db.ExamStems.Include(exs => exs.Exam)
                .Include(exs => exs.Stem).ToListAsync<ExamStem>();
        }

        // Get all Exam Stems for a specific exam
        public async Task<IEnumerable<ExamStem>?> GetExamStemsByExam(Exam exam)
        {
            return await _db.ExamStems.Where(exs => exs.Exam == exam)
                .Include(exs => exs.Exam.Certificate)
                .Include(exs => exs.Exam.CandidateExams)
                .Include(exs => exs.Exam).Include(exs => exs.Stem)
                .Include(exs => exs.Stem.Topic).ToListAsync<ExamStem>();
        }
        public async Task<IEnumerable<int>?> GetStemIdsByExam(Exam exam)
        {
            return await _db.ExamStems.Where(exs => exs.Exam == exam)
                .Select(exs => exs.Id).ToListAsync();
        }


        public EntityState AddOrUpdate(ExamStem examStem)
        {
            _db.ExamStems.Update(examStem);
            return _db.Entry(examStem).State;
        }

        public void Delete(ExamStem examStem)
        {
            _db.ExamStems.Remove(examStem);
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.ExamStems.AnyAsync(e => e.Id == id);
        }           
    }
}
