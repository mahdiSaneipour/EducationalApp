using EP.Core.DTOs.ForumViewModel;
using EP.Core.Interfaces.Forum;
using EP.Core.Tools.Security;
using EP.Domain.Entities.Question;
using EP.Domain.Interfaces.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Forum
{
    public class UserForumServices : IUserForumServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public UserForumServices(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public int AddQuestion(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.ModifiedDate = DateTime.Now;

            _questionRepository.AddQuestion(question);
            _questionRepository.SaveChanges();

            return question.QuestionId;
        }

        public ShowForumViewModel GetQuestionAndAnswersByQuestionId(int questionId)
        {
            ShowForumViewModel forum = new ShowForumViewModel();

            forum.Question = _questionRepository.GetQuestionByQuestionId(questionId);
            forum.Answers = _answerRepository.GetAnswersByQuestionId(questionId);

            return forum;
        }

        public int AddAnswer(int userId, int questionId, string answerBody)
        {
            Answer answer = new Answer() {
                UserId = userId,
                QuestionId = questionId,
                BodyAnswer = answerBody.SanitizeHtml(),
                CreateDate = DateTime.Now
            };

            _answerRepository.AddAnswer(answer);
            _answerRepository.SaveChanges();

            return answer.AnswerId;
        }

        public void SetAnswerForQuestion(int userId, int answerId, int questionId)
        {
            if (_questionRepository.IsQuestionForUser(userId, questionId))
            {
                Question question = _questionRepository.GetQuestionByQuestionId(questionId);

                question.AnswerId = answerId;

                _questionRepository.UpdateQuestion(question);
                _questionRepository.SaveChanges();
            }
        }

        public Tuple<List<Question>, int> GetQuestions(int? courseId = 0, int? pageId = 1)
        {
            IQueryable<Question> questions = _questionRepository.GetAllQuestions();

            int take = 10;
            int skip = (int)((pageId - 1) * take);

            if (courseId != 0)
            {
                Console.WriteLine("ttttttttttttt");
                questions = questions.Where(q => q.CourseId == courseId);
            }

            int pageCount = questions.Count() / take;

            if (questions.Count() % 10 != 0)
            {
                pageCount++;
            }

            Tuple<List<Question>,int> result = Tuple.Create(questions.Skip(skip).Take(take).ToList(), pageCount);

            return result;
        }
    }
}
