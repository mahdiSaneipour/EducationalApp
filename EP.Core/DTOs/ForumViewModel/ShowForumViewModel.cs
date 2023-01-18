using EP.Domain.Entities.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.ForumViewModel
{
    public class ShowForumViewModel
    {
        public Question Question { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
