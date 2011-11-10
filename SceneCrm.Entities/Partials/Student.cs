using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneCrm.Entities
{
    public partial class Student
    {
        public String Name
        {
            get { return Forename + " " + Surname; }
        }
    }
}
