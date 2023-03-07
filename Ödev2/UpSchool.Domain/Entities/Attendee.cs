using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Common;

namespace UpSchool.Domain.Entities
{
    public class Attendee : EntityBase
    {
        public string FullName { get; set; }
    }
}
