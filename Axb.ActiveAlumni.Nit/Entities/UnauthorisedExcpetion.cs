using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class UnauthorisedExcpetion : Exception
    {
        public UnauthorisedExcpetion()
            :base(Strings.UnAuthAccess)
        {

        }
    }
}