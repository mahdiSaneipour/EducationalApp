using EP.Core.DTOs.ForumViewModel;
using EP.Domain.Entities.Question;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Forum
{
    public interface IUserForumServices
    {
        public int AddQuestion(Question question);

        public ShowForumViewModel GetQuestionAndAnswersByQuestionId(int questionId);

        public int AddAnswer(int userId, int questionId, string answerBody);

        public void SetAnswerForQuestion(int userId, int answerId, int questionId);

        public Tuple<List<Question>, int> GetQuestions(int? courseId = 0, int? pageId = 1);
    }
}
