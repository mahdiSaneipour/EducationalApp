using EP.Domain.Entities.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Forum
{
    public interface IQuestionRepository
    {
        public void AddQuestion(Question question);

        public Question GetQuestionByQuestionId(int questionId);

        public void UpdateQuestion(Question question);

        public bool IsQuestionForUser(int userId, int questionId);
        
        public IQueryable<Question> GetAllQuestions();

        public void SaveChanges();
    }
}
