using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    [Serializable]
    public class SimpleException : Exception
    {
        public SimpleException(string message)
            : base(message)
        {
        }

        public SimpleException(
            string message,
            params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}