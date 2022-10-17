using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.DTO.Common
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public ICollection<string> Messages { get; set; }
        public object? Data { get; set; }

        public Response()
        {
            Status = ResponseStatus.Success;
            Messages = new List<string>();
            Data = null;
        }

        public static Response FromException(Exception ex, object? data = null)
        {
            return new Response
            {
                Status = ResponseStatus.Error,
                Messages = new List<string>{ ex.Message ?? "Unknown error." },
                Data = data
            };
        }
    }

    public enum ResponseStatus
    {
        Success,
        Error
    }
}
