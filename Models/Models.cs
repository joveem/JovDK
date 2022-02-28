using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Models
{

    public class Login
    {

        public class LoginRequest
        {
            public string login = "";
            public string password = "";
            public string token = "";
        }

        public class LoginResponse
        {
            public string token = "";
            public int id = 0;
            public string name = "";

        }

    }



    public class VersionRequest
    {

        public int version = 0;

    }

    public class VersionResponse
    {
        public string apiUrl;
        public string socketUrl;
        public versionStatus status;

    }
    public enum versionStatus
    {
        Updated = 0,

        Outdated,

        Blocked
    }

}
