using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Errors
{
    public class AppError : Exception
    {
        private readonly HttpStatusCode StatusCode;

        public AppError(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
