using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticAPI.Dtos
{
    /// <summary>
    /// Universal response for all potential end points
    /// </summary>
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string ErrorMsg { get; set; } = "";

    }
}
