using System;
using System.Net;

namespace Framework
{
    public  class ApiEnumAttribute : Attribute
    {
        public ApiEnumAttribute(HttpStatusCode code)
        {
            this.HttpStatusCode = code;
        }
        public HttpStatusCode HttpStatusCode { get; private set; }
    }
}