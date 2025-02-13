using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.BuitIn.Cloud
{
    [ApiController]
    [Route("api/buit-in/upload-files")]
    public class UploadFileController(IUploadFileBySignature uploadFileBySignatureService)
        : ControllerBase
    {
        // you can use factory make create url upload images to something else cloud
        [HttpGet("get-url-by-signature")]
        public ActionResult<string> GetUrlBySignature()
        {
            var url = uploadFileBySignatureService.GetUrl();
            return Ok(url);
        }
    }
}