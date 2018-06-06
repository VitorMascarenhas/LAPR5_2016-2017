using Porto.Go.Monitor.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porto.Go.Monitor.Service.Helpers
{
    public interface IApiService
    {
        TokenResponse TokenReponse { get; }

        void Login();

        IEnumerable<UserDto> GetUsers();
    }
}
