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
            [Display(Name = "the input data is not correct")]
            [ApiEnum(System.Net.HttpStatusCode.BadRequest)]
            InvalidDataInput = 1,
            [Display(Name = "Authorization is missing")]
            [ApiEnum(System.Net.HttpStatusCode.Unauthorized)]
            AuthorizationMissing = 2,
            [Display(Name = "Authorization is invalid")]
            [ApiEnum(System.Net.HttpStatusCode.Unauthorized)]
            AuthorizationInvalid = 3,
            [Display(Name = "Authorization is invalid ")]
            [ApiEnum(System.Net.HttpStatusCode.InternalServerError)]
            InternalError = 4,

        }
    
}
