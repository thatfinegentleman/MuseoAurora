using System;
using System.Collections.Generic;
using System.Text;

namespace MuseoAurora.Models
{
    public class InsertResult<T>
    {
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess => ErrorMessage == null;
    }
}