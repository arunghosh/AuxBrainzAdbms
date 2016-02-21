using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Services
{
    public interface ICryptographyService
    {
        string CreateHash(string input);
        bool IsHashSame(string input, string hash);

        string CreateTokenWithUserId(int userId, int length);
        string GetCodeFromToken(string token);
        int GetUserIdFromToken(string token);

    }

}