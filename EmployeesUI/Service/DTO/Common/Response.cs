using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTO.Common
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

        public bool IsSuccessful() => Status == ResponseStatus.Success;

        public string Message() => string.Join("\n", Messages);
    }

    public enum ResponseStatus
    {
        Success,
        Error
    }
}
