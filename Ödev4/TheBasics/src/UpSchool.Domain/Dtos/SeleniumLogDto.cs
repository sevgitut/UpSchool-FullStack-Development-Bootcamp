using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.Domain.Dtos
{
    public class SeleniumLogDto
    {
        public string Message { get; set; }
        public DateTimeOffset SentOn { get; set; }

        public SeleniumLogDto(string message)
        {
            Message = message;

            SentOn = DateTimeOffset.Now;
        }
    }
}
