using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.JWT.Services
{
    public interface IPasswordService
    {
        public void CreatePasswordHash (string username, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
