using System;
namespace AegisImplicitMail
{
    public class ServerException : Exception
    {
        public ServerException(String msg):base(msg)
        {
        }
    }
}
