using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class suscripcionMam
    {
        public dynamic insertMam()
        {
            string json = @"
                            {
	                        ""rootSchemaName"": ""BbmMAMMembership"",
                            ""columnValues"": {
                            ""items"": {
                            ""BbmMAMAffiliate"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""idcrm2""
                            }
                            },
                            ""BbmMAMMembershipType"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""id_Tipo_Suscrip_Mam_Crm2""
                            }
                            },
                            ""BbmMAMStartDateMembership"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 8,
                            ""value"": ""\""Fecha_Suscrip_MAM2\""""
                            }
                            },
                            ""BbmMAMMembershipStatus"": {
                            ""expressionType"": 2,
                            ""parameter"": {
                            ""dataValueType"": 10,
                            ""value"": ""id_Status_Mam_Crm2""
                            }
                            }
                            }
                            }
                            }
                            ";

            return json;
        }

        public dynamic BuscarEstadoMam()
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
                            ""BbmContactMAMMenbership"": {
                            ""expression"": {
                            ""columnPath"": ""BbmContactMAMMenbership""
                            }
                            }
                            }
                            },
                            ""ignoreDisplayValues"": true
                            ";
                            return json;
        }





    }
}
