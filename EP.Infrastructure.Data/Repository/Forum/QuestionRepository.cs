using EP.Domain.Entities.Question;
using EP.Domain.Interfaces.Forum;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Forum
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EPContext _context;

        public QuestionRepository(EPContext context)
        {
            _context = context;
        }

        public void AddQuestion(Question question)
        {
            _context.Questions.Add(question);
        }

        public Question GetQuestionByQuestionId(int questionId)
        {
            return _context.Questions.Where(q => q.QuestionId == questionId)
                .Include(q => q.User)
                .Include(q => q.Course).FirstOrDefault();
        }

        public void UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
        }

        public bool IsQuestionForUser(int userId, int questionId)
        {
            return _context.Questions.Any(q => q.QuestionId == questionId && q.UserId == userId);
        }

        public IQueryable<Question> GetAllQuestions()
        {
            return _context.Questions.Include(q => q.User).Include(q => q.Course);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
