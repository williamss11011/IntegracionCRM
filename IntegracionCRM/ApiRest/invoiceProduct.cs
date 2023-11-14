using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class invoiceProduct
    {
        public dynamic facturaDetalle()
        {
            string json =   @"
                            {
	                        ""rootSchemaName"": ""InvoiceProduct"",
                            ""columnValues"": {
                            ""items"": {
                            ""Invoice"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""id_venta_cabecera_crm2""
                            }
                            },
                            ""Product"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""codigoItem2""
                            }
                            },
                            ""PriceList"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""fa689c95-c63c-4908-8fd2-19a95e0425bd""
                            }
                            },
                            ""Price"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": pvp2
                            }
                            },
                            ""Quantity"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 5,
                            ""value"": cantidad2
                            }
                            },
                            ""Amount"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": monto2   
                            }
                            },
                            ""DiscountPercent"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": 0
                            }
                            },
                            ""DiscountAmount"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": 0
                            }
                            },
                            ""Tax"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""5a52f3f4-c53b-4edb-9d64-6861c80adcb9""
                            }
                            },
                            ""DiscountTax"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 5,
                            ""value"": 0
                            }
                            },
                            ""TaxAmount"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": impuesto2    
                            }
                            },
                            ""TotalAmount"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 6,
                            ""value"": valor2    
                            }
                            }
                            }
                            }
                            }
                            ";

            return json;
        }
    }
}
