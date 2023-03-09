using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Collections;

namespace CustomFieldsManager.Services
{
    public class CustomFieldsService : ICustomFieldsService
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public CustomFieldsService()
        {
            //_configuration = configuration;
            string filepath = @"\test-2a07f-4688daf8c712.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);

            projectId = "test-2a07f";
            fireStoreDb = FirestoreDb.Create(projectId);
        }



        public async Task<CustomFields> CreateCustomField(CustomFields customField)
        {
            try
            {
                dynamic MultipleChoiceOptions;

                if (customField.FieldType == 1)
                {
                    MultipleChoiceOptions = Convert.ToString(customField.MultipleChoiceOptions);
                }
                if (customField.FieldType == 2)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                    MultipleChoiceOptions = Convert.ToInt64(data);
                }
                else if (customField.FieldType == 3)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                    MultipleChoiceOptions = Convert.ToBoolean(data);
                }
                else if (customField.FieldType == 4)
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                    MultipleChoiceOptions = new ArrayList();
                    foreach (var field in data)
                    {
                        MultipleChoiceOptions.Add(field.Value);
                    }
                }
                else
                {
                    MultipleChoiceOptions = Convert.ToString(customField.MultipleChoiceOptions);
                }

                var customFieldsSave = new CustomFields()
                {
                    ClientId = customField.ClientId,
                    FieldName = customField.FieldName,
                    FieldType = customField.FieldType,
                    IsVisibleConditionFieldId = customField.IsVisibleConditionFieldId,
                    IsVisibleConditionFieldValue = customField.IsVisibleConditionFieldValue,
                    MultipleChoiceOptions = MultipleChoiceOptions
                };

                CollectionReference colRef = fireStoreDb.Collection("customfields");
                var result = await colRef.AddAsync(customFieldsSave);

                customField.Id = result.Id;
                return customField;

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        public async Task<List<CustomFields>> GetAllCustomFields()
        {
            Query customFieldQuery = fireStoreDb.Collection("customfields");
            QuerySnapshot customFieldQuerySnapshot = await customFieldQuery.GetSnapshotAsync();
            List<CustomFields> customFieldsList = new List<CustomFields>();

            foreach (DocumentSnapshot documentSnapshot in customFieldQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    CustomFields customFields = documentSnapshot.ConvertTo<CustomFields>();
                    customFields.Id = documentSnapshot.Id;
                    customFieldsList.Add(customFields);
                }
            }
            return customFieldsList;

        }

        public async Task<CustomFields> UpdateCustomField(CustomFields customField)
        {
            dynamic MultipleChoiceOptions;

            if (customField.FieldType == 1)
            {
                MultipleChoiceOptions = Convert.ToString(customField.MultipleChoiceOptions);
            }
            if (customField.FieldType == 2)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                MultipleChoiceOptions = Convert.ToInt64(data);
            }
            else if (customField.FieldType == 3)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                MultipleChoiceOptions = Convert.ToBoolean(data);
            }
            else if (customField.FieldType == 4)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(customField.MultipleChoiceOptions.ToString());

                MultipleChoiceOptions = new ArrayList();
                foreach (var field in data)
                {
                    MultipleChoiceOptions.Add(field.Value);
                }
            }
            else
            {
                MultipleChoiceOptions = Convert.ToString(customField.MultipleChoiceOptions);
            }




            var customFieldsUpdate = new CustomFields()
            {
                ClientId = customField.ClientId,
                FieldName = customField.FieldName,
                FieldType = customField.FieldType,
                IsVisibleConditionFieldId = customField.IsVisibleConditionFieldId,
                IsVisibleConditionFieldValue = customField.IsVisibleConditionFieldValue,
                MultipleChoiceOptions = MultipleChoiceOptions
            };
            DocumentReference customFields = fireStoreDb.Collection("customfields").Document(customField.Id);
            await customFields.SetAsync(customFieldsUpdate, SetOptions.Overwrite);
            return customFieldsUpdate;

        }
        public async Task<CustomFields> GetCustomFieldsById(string customFieldId)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("customfields").Document(customFieldId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    CustomFields customField = snapshot.ConvertTo<CustomFields>();
                    customField.Id = snapshot.Id;
                    return customField;
                }
                else
                {
                    return new CustomFields();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }
        public async Task<string> DeleteCustomField(string customFieldId)
        {
            try
            {
                DocumentReference customFields = fireStoreDb.Collection("customfields").Document(customFieldId);
                await customFields.DeleteAsync();
                return "Deleted Successfully!";
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }
    }
}
