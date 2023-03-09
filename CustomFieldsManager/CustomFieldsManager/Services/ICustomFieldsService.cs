namespace CustomFieldsManager.Services
{
    public interface ICustomFieldsService
    {
        Task<CustomFields> CreateCustomField(CustomFields customField);
        Task<List<CustomFields>> GetAllCustomFields();
        Task<CustomFields> UpdateCustomField(CustomFields customField);
        Task<string> DeleteCustomField(string customFieldId);
        Task<CustomFields> GetCustomFieldsById(string customFieldId);
    }
}
