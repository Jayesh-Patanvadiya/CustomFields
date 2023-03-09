using CustomFieldsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomFieldsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFieldController : ControllerBase
    {
        ICustomFieldsService _customFieldsService;
        public CustomFieldController(ICustomFieldsService customFieldsService)
        {
            _customFieldsService = customFieldsService;
        }
        [HttpPost]
        public async Task<CustomFields> CreateCustomField([FromBody] CustomFields customField)
        {

            var createResult = await _customFieldsService.CreateCustomField(customField);
            return createResult;

        }
        [HttpGet]
        public async Task<List<CustomFields>> GetAllCustomFields()
        {
            return await _customFieldsService.GetAllCustomFields();
        }

        [HttpGet("customFieldid")]
        public async Task<CustomFields> GetCustomFieldsById(string customFieldid)
        {
            return await _customFieldsService.GetCustomFieldsById(customFieldid);
        }


        [HttpPut]
        public async Task<CustomFields> UpdateCustomFields([FromBody] CustomFields customField)
        {

            return await _customFieldsService.UpdateCustomField(customField);
        }

        [HttpDelete]
        public async Task<string> DeleteCustomFields(string customFieldid)
        {
            return await _customFieldsService.DeleteCustomField(customFieldid);
        }
    }
}
