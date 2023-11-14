using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class consultaContacto
    {
        public dynamic ConsultContact()
        {
            string json = @"
                {
                ""rootSchemaName"": ""Contact"",
                ""filters"": {
                ""items"": {
                ""ByCedula"": {
                ""filterType"": 1,
                ""comparisonType"": 3,
                ""isEnabled"": true,
                ""leftExpression"": {
                ""expressionType"": 0,
                ""columnPath"": ""BbmContactDocumentID""
                },
                ""rightExpression"": {
                ""expressionType"": 2,
                ""parameter"": {
                ""dataValueType"": 1,
                ""value"": ""CEDULA""
                }
                }
                }
                },
                ""logicalOperation"": 0,
                ""isEnabled"": true,
                ""filterType"": 6,
                ""rootSchemaName"": ""Contact""
                },
                ""columns"": {
                ""items"": {
                ""Id"": {
                ""expression"": {
                ""columnPath"": ""Id""
                }
                },
                ""Name"": {
                ""expression"": {
                ""columnPath"": ""Name""
                }
                },
                ""Email"": {
                ""expression"": {
                ""columnPath"": ""Email""
                }
                }
                }
                },
                ""ignoreDisplayValues"": true
                }";


     

            return json;
        }
    }
}
