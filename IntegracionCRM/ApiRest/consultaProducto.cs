using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class consultaProducto
    {
        public dynamic ConsultItem()
        {
            string json = @"
                        {
                        ""rootSchemaName"": ""Product"",
                        ""filters"": {
                        ""items"": {
                        ""ByCode"": {
                        ""filterType"": 1,
                        ""comparisonType"": 3,
                        ""isEnabled"": true,
                        ""leftExpression"": {
                        ""expressionType"": 0,
                        ""columnPath"": ""Code""
                        },
                        ""rightExpression"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""CODITEM""
                        }
                        }
                        }
                        },
                        ""logicalOperation"": 0,
                        ""isEnabled"": true,
                        ""filterType"": 6,
                        ""rootSchemaName"": ""Product""
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
                        ""columnPath"": ""Price""
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
