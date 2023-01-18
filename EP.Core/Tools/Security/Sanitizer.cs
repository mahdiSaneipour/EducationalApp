using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Tools.Security
{
    public static class Sanitizer
    {
        public static string SanitizeHtml(this string html)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(html);
        }
    }
}
