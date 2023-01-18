using EP.Domain.Entities.Question;
using EP.Domain.Interfaces.Forum;
using EP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Forum
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly EPContext _context;

        public AnswerRepository(EPContext context)
        {
            _context = context;
        }

        public List<Answer> GetAnswersByQuestionId(int questionId)
        {
            return _context.Answer.Where(a => a.QuestionId == questionId)
                .OrderByDescending(a => a.CreateDate).ToList();
        }

        public void AddAnswer(Answer answer)
        {
            _context.Answer.Add(answer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
