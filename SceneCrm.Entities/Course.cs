using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneCrm.Entities
{
    public partial class Course
    {
        public String DisplayName
        {
            get { return this.CourseType.CourseTypeName + " - " + this.Term.TermName + " - " + this.Year; }
        }
    }
}
