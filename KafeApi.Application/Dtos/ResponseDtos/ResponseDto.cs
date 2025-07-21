namespace KafeApi.Application.Dtos.ResponseDtos;

public class ResponseDto<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public string ErrorCodes { get; set; }
}

//public enum StatusCode
//{
//    Ok = 200,
//    Created = 201,
//    NoContent = 204,
//    BadRequest = 400,
//    Unauthorized = 401,
//    Forbidden = 403,
//    NotFound = 404,
//    InternalServerError = 500
//}
