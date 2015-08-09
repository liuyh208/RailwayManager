using System;
using System.Collections.Generic;
using System.IO;
using ServiceStack.Common.Web;
using ServiceStack.Service;
using ServiceStack.ServiceHost;
using ServiceStack.Text;

namespace GasWebMap.Services.Responses
{
    [Serializable]
    public class ResponseResult
    {
        /// <summary>
        ///     The success resource
        /// </summary>
        public static ResponseResult SuccessRes = new ResponseResult
        {
            //todo 增加授权

            Success = true
        };

        public bool Success { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }

        public static ResponseResult FailureRes(string msg)
        {
            return new ResponseResult {Success = false, Msg = msg};
        }
    }

    public class ExcelResult : IHasOptions, IStreamWriter
    {
        public IDictionary<string, string> Options { get; private set; }
        Stream _responseStream;

        public ExcelResult(Stream responseStream)
        {
            _responseStream = responseStream;

            long length = -1;
            try { length = _responseStream.Length; }
            catch (NotSupportedException) { }


                Options = new Dictionary<string, string> {
             {"Content-Type", "application/octet-stream"},
             {HttpHeaders.ContentDisposition, "attachment; filename=data.xls;"}
         };
        }

        public void WriteTo(Stream responseStream)
        {
            if (_responseStream == null)
                return;

            using (_responseStream)
            {
                _responseStream.WriteTo(responseStream);
                responseStream.Flush();
            }
        }
    }
}