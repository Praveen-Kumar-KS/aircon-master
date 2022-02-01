using Aircon.My7LApi.Model;
using Aircon.My7LApi.RESTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.My7LApi.Api
{
    public class UserApi : RootApi<UserRequest, UserResponse>
    {
        public UserApi(Configuration configuration) : base(configuration)
        { }

        public override string GetEndpointPath()
        {
            return "/api/v1/users";
        }
    }
}
