using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Authentication.ResponseMessages
{
    public class Message
    {
        //ErrorMessages
        public static string[] UserExists = { "Error", "User already exists" };
        public static string[] UserValidation = {"Error", "User creation failed! Please check user details and try again." };

        //SuccessMessages
        public static string[] UserCreated = { "Success", "User created successfully!" };


    }
}
