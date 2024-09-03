using Microsoft.AspNetCore.Mvc;

namespace Trasen.PaperFree.Domain.Shared.Response
{
    public class ObjectResponse
    {
        public static OkObjectResult Error(string message)
        {
            return new OkObjectResult(new ApiResult<object>(MessageType.Error, ResultCode.FAIL, message, default, false));
        }

        [NonAction]
        public static OkObjectResult Ok(string message)
        {
            return new OkObjectResult(new ApiResult<object>(MessageType.Success, ResultCode.SUCCESS, message, default, false));
        }

        [NonAction]
        public static OkObjectResult Ok<T>(string message, T data)
        {
            return new OkObjectResult(new ApiResult<object>(MessageType.Success, ResultCode.SUCCESS, message, data, false));
        }

        [NonAction]
        public static OkObjectResult Ok<T>(MessageType messageType, string message, T data)
        {
            return new OkObjectResult(new ApiResult<object>(messageType, ResultCode.SUCCESS, message, data,false));
        }

        [NonAction]
        public static OkObjectResult Ok<T>(MessageType messageType, string message, ResultCode resultCode, T data)
        {
            return new OkObjectResult(new ApiResult<object>(messageType, resultCode, message, data,false));
        }
    }
}