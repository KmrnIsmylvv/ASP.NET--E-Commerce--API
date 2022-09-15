using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse

    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
        
    }
}