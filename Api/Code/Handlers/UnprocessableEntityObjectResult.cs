using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Helpers
{
    public class UnprocessableEntityObjectResult : ObjectResult
    {
        public UnprocessableEntityObjectResult(Dictionary<string, List<string>> errors)
            : base(errors)
        {
            if(errors == null)
            {
                throw new Exception("Validation failed with no errors.");
            }
            StatusCode = 422;
        }
    }
}
