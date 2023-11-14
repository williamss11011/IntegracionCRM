using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class invoice
    {
        public dynamic factura()
        {
            string json = @"{
	                        ""rootSchemaName"": ""Invoice"",
                            ""columnValues"": {
                            ""items"": {
                            ""StartDate"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 8,
                            ""value"": ""\""fecha2\""""
                            }
                            },
			                ""PaymentStatus"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""698d39fd-52e6-df11-971b-001d60e938c6""
                            }
                            },
                            ""BbmInvoiceTransactionNumber"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 1,
                            ""value"": ""transaccion2""
                            }
                            },
                            ""BbmInvoiceTransactionType"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""tipo2""
                            }
                            },
                            ""BbmInvoiceStoreCode"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 1,
                            ""value"": ""tienda2""
                            }
                            },
                            ""BbmInvoiceCashRegisterNumber"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 1,
                            ""value"": ""caja2""
                            }
                            },                                                                                                                                                                              
                            ""BbmInvoiceContact"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""contacto2""
                            }
                            },
                            ""Contact"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""contacto2""
                            }
                            }
                            }
                            }
                            }";

            return json;
        }
    }
}
