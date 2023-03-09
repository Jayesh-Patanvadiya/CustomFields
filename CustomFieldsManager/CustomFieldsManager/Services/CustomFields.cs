using Google.Cloud.Firestore;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomFieldsManager.Services
{
    [FirestoreData]

    public class CustomFields
    {
        public string Id { get; set; } // firebase unique id

        [FirestoreProperty]
        [Required]
        public string? FieldName { get; set; }


        [FirestoreProperty]
        [Required]
        public int ClientId { get; set; }
        //Field Type - int (1 = string; 2 = number; 3 =bool 4 = multiple choice)
       // MultipleChoiceOptions(array of strings - mandatory if fieldType= 4)
        
        [FirestoreProperty]
        public int FieldType { get; set; }

        [FirestoreProperty]
        public dynamic MultipleChoiceOptions { get; set; }

        [FirestoreProperty]
        public int IsVisibleConditionFieldId { get; set; }

        [FirestoreProperty]
        public string? IsVisibleConditionFieldValue { get; set; }
    }
}
