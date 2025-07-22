using KafeApi.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        // Generic bir response'ı değerlendirip uygun HTTP cevabını döndüren yardımcı metot
        protected IActionResult CreateResponse<T>(ResponseDto<T> response) where T : class
        {
            // Eğer işlem başarılıysa, 200 OK sonucu döndür
            if (response.Success)
            {
                return Ok(response);
            }

            // İşlem başarısızsa, ErrorCode değerine göre uygun HTTP hatası döndür
            return response.ErrorCode switch
            {
                ErrorCodes.NotFound => NotFound(response),                        
                ErrorCodes.ValidationError => BadRequest(response),            
                ErrorCodes.Unauthorized => Unauthorized(response),              
                ErrorCodes.Forbidden => StatusCode(StatusCodes.Status403Forbidden, response), 
                ErrorCodes.Exception => StatusCode(StatusCodes.Status500InternalServerError, response), 
                ErrorCodes.DublicationError => Conflict(response),                
                ErrorCodes.BadRequest => BadRequest(response),                    
                _ => BadRequest(response)                                         
            };
        }

    }
}
