using System;

namespace Web.Middleware
{
    [Serializable]
    public class SuccessResponse
    {
        public int? StatusCode { get; set; }
        public object Results { get; set; }
        public object Messages { get; set; }

    }
}