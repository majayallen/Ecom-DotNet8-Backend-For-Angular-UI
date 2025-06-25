﻿namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(statusCode);
        }
        private string GetMessageFromStatusCode(int code)
        {
            return code switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "UN Authorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null
            };
        }

        public int StatusCode {  get; set; }
        public string? Message {  get; set; }
    }
}
