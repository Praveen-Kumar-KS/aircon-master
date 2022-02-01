using Aircon.My7LApi.Api;
using Aircon.My7LApi.Model;
using Aircon.My7LApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.My7LApi
{
    public static class Test
    {
        public static void GetModelItems(string filter)
        {
            var authApi = new AuthApi("https://restapi.my7l.com/");
            try
            {
                var configuration = authApi.LogIn("aircon.demo", "aircon.demo");
                Console.WriteLine(configuration.AccessToken);
                var userApi = new UserApi(configuration);
                var result = userApi.RootGet(new UserRequest());
                Console.WriteLine(result.data.message);
            }
            catch (Exception ex)
            {
                throw new Exception("Encountered an error during GetList" + ex.Message);
            }
            finally
            {
                //authApi.AuthLogout();
            }


        }

    }
}
