using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public interface IDigestService: IDisposable
    {
        IEnumerable<IDigestEntity> GetDigest(int userId);
        string GetDigestTitle();
    }
}