using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class LoginWithEmailModel
    {
        public string email;
        public string password;
    }

    public class LoginWithPlayFabModel
    {
        public string username;
        public string password;
    }

    public class LoginWithGoogleModel
    {
        public string serverAuthCode;
    }

    #region Results

    public class Result
    {
        public bool isSuccess;
        public object message;
    }

    #endregion
}