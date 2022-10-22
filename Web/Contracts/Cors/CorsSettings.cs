using System.Linq;

namespace Web.Contracts.Cors
{
    public class CorsSettings
    {
        public string[] Headers { get; set; }
        public string[] Methods { get; set; }
        public string[] Origins { get; set; }

        public long? PreflightMaxAge { get; set; }
        public bool HasValue
        {
            get
            {
                if (Headers == null || Headers.Any() == false)
                {
                    return false;
                }
                if (Methods == null || Methods.Any() == false)
                {
                    return false;
                }
                if (PreflightMaxAge.GetValueOrDefault() <= 0)
                {
                    return false;
                }
                return true;
            }

        }


    }
}
