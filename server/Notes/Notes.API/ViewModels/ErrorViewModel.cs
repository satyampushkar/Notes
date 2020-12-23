using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Notes.API.ViewModels
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        //public ErrorViewModel(int statusCode, string message)
        //{
        //    StatusCode = statusCode;
        //    Message = message;
        //}
        //public override string ToString()
        //{
        //    return JsonSerializer.Serialize(this);//JsonConvert.SerializeObject(this);
        //}
    }
}
