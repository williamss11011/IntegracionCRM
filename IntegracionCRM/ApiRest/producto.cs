using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class producto
    {
        public dynamic product()
        {
            string json = @"
                        {
	                    ""rootSchemaName"": ""Product"",
                        ""columnValues"": {
                        ""items"": {
                        ""Name"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""NombreItem""
                        }
                        },
                        ""Code"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""CodigoItem""
                        }
                        },
                        ""BbmProductItemID"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""IdItem""
                        }
                        },
                        ""BbmProductBarcode"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""CodigoBarras""
                        }
                        },
                        ""BbmProductDivision"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""IdDivision""
                        }
                        },
                        ""BbmProductsubdivision"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""IdSubdivision""
                        }
                        },
                        ""BbmProductDepartment"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""IdDepartamento""
                        }
                        },
                        ""BbmProductSection"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""IdSeccion""
                        }
                        },
                        ""Price"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 6,
                        ""value"": ""Precio""
                        }
                        }
                        }
                        }
                        }";

            return json;
        }
    }
}
