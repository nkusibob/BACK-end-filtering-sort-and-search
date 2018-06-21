using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainPsr03951
{
    
        public class ResponseMessage
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public ApiEnum apiEnum { get; set; }
            public string Message { get; set; }
            public List<ResponseError> Errors { get; set; }
        }
    
}
