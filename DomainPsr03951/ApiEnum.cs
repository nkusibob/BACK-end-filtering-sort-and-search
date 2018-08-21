using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;
using System.ComponentModel.DataAnnotations;



namespace DomainPsr03951
{
   
        public enum ApiEnum
        {
            [Display(Name = "For the server the input data is not correct, bad syntax")]
            [ApiEnum(System.Net.HttpStatusCode.BadRequest)]
            InvalidDataInput = 1,
            [Display(Name = "Authorization is missing")]
            [ApiEnum(System.Net.HttpStatusCode.Unauthorized)]
            AuthorizationMissing = 2,
            [Display(Name = "Authorization is missing")]
            [ApiEnum(System.Net.HttpStatusCode.Forbidden)]
            AuthorizationInvalid = 3,
            [Display(Name = "server didn't find the ressources ")]
            [ApiEnum(System.Net.HttpStatusCode.NotFound)]
            InternalError = 4,

        }
    
}
