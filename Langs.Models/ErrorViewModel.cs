using System;

namespace Langs.Models
{
    public class ErrorViewModel
    {
        public Exception Exception { get; set; }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}