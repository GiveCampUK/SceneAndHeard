using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneCrm.Entities
{
    public partial class Volunteer
    {
        public String Name
        {
            get { return this.FirstName + " " + this.Surname; }

        }

    }
}
