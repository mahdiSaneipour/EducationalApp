using EP.Domain.Entities.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Forum
{
    public interface IAnswerRepository
    {
        public List<Answer> GetAnswersByQuestionId(int questionId);

        public void AddAnswer(Answer answer);

        public void SaveChanges();
    }
}
