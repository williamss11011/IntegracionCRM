using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracionCRM.ApiRest
{
    class contacto
    {
        public dynamic contact()
        {
            string json = @"
                          {
	                    ""rootSchemaName"": ""Contact"",
                        ""columnValues"": {
                        ""items"": {
                        ""GivenName"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""nombre2""
                        }
                        },
                        ""Surname"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""apellido2""
                        }
                        },
			            ""Email"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""mail2""
                        }
                        },
			            ""MobilePhone"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""telefono2""
                        }
                        },
                        ""Type"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoContacto2""
                        }
                        },
                        ""BbmContactDocumentType"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoDocumento2""
                        }
                        },
                        ""BbmContactDocumentID"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""documento2""
                        }
                        },
                        ""BbmContactPregnancyWeeks"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 4,
                        ""value"": ""semana""
                        }
                        },
                        ""BbmContactAceptacionUsoDatos"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 12,
                        ""value"": ""bool""
                        }
                        },""BbmContactFechaAceptacionUsoDatos"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 8,
                        ""value"": ""\""date\""""
                        }
                        }
                        }
                        }
                        }";

            return json;
        }


        public dynamic contactSF()   // contacto sin fecha 
        {
            string json = @"
                          {
	                    ""rootSchemaName"": ""Contact"",
                        ""columnValues"": {
                        ""items"": {
                        ""GivenName"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""nombre2""
                        }
                        },
                        ""Surname"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""apellido2""
                        }
                        },
			            ""Email"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""mail2""
                        }
                        },
			            ""MobilePhone"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""telefono2""
                        }
                        },
                        ""Type"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoContacto2""
                        }
                        },
                        ""BbmContactDocumentType"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoDocumento2""
                        }
                        },
                        ""BbmContactDocumentID"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""documento2""
                        }
                        },
                        ""BbmContactPregnancyWeeks"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 4,
                        ""value"": ""semana""
                        }
                        },
                        ""BbmContactAceptacionUsoDatos"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 12,
                        ""value"": ""bool""
                        }
                        }
                        
                        }
                        }
                        }";

            return json;
        }


        public dynamic contact2()
        {
            string json = @"
                          {
	                    ""rootSchemaName"": ""Contact"",
                        ""columnValues"": {
                        ""items"": {
                        ""GivenName"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""nombre2""
                        }
                        },
                        ""Surname"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""apellido2""
                        }
                        },
			            ""Email"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""mail2""
                        }
                        },
			            ""MobilePhone"": {
                        ""expressionType"": 2,
				        ""parameter"": {
                        ""dataValueType"": 1,
					    ""value"": ""telefono2""
                        }
                        },
                        ""Type"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoContacto2""
                        }
                        },
                        ""BbmContactDocumentType"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 10,
                        ""value"": ""tipoDocumento2""
                        }
                        },
                        ""BbmContactDocumentID"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 1,
                        ""value"": ""documento2""
                        }
                        },
                        ""BbmContactPregnancyWeeks"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 4,
                        ""value"": ""semana""
                        }
                        },
                        ""BbmContactAceptacionUsoDatos"": {
                        ""expressionType"": 2,
                        ""parameter"": {
                        ""dataValueType"": 12,
                        ""value"": bool
                        }
                        }
                        }
                        }
                        }";

            return json;
        }


        public dynamic contactsQuantity()
        {
            string jsonContactsQuantity = @"

            {
            ""rootSchemaName"": ""Contact"",
            ""operationType"": 0,
            ""includeProcessExecutionData"": true,
            ""filters"":{
                ""items"": {
                    ""ByCreatedOn"": {
                        ""filterType"": 1,
                ""comparisonType"": 8,
                ""isEnabled"": true,
                ""leftExpression"": {
                            ""expressionType"": 0,
                    ""columnPath"": ""CreatedOn""
                },
                ""rightExpression"": {
                            ""expressionType"": 2,
                    ""parameter"": {
                                ""dataValueType"": 8,
                        ""value"": ""\""FechaCreacion\""""
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
                    ""IdCOUNT"": {
                        ""caption"": """",
                ""orderDirection"": 0,
                ""orderPosition"": -1,
                ""isVisible"": true,
                ""expression"": {
                            ""expressionType"": 1,
                    ""functionType"": 2,
                    ""functionArgument"": {
                                ""expressionType"": 0,
                        ""columnPath"": ""Id""
                    },
                    ""aggregationType"": 1
                }
                    }
                }
            },
            ""serverESQCacheParameters"": {
                ""cacheLevel"": 0,
            ""cacheGroup"": """",
            ""cacheItemName"": """"
            },
            ""useMetrics"": false,
            ""adminUnitRoleSources"": 0,
            ""ignoreDisplayValues"": false,
            ""isHierarchical"": false
            }          
             ";

            return jsonContactsQuantity;
        }



        public dynamic recuperaContactosCRM()
        {
            string jsonContactsCRM = @"
            {
            ""rootSchemaName"": ""Contact"",
            ""filters"":{
                ""items"": {
                    ""ByCreatedOn"": {
                        ""filterType"": 1,
                ""comparisonType"": 8,
                ""isEnabled"": true,
                ""leftExpression"": {
                            ""expressionType"": 0,
                    ""columnPath"": ""CreatedOn""
                },
                ""rightExpression"": {
                            ""expressionType"": 2,
                    ""parameter"": {
                                ""dataValueType"": 8,
                        ""value"": ""\""FechaCreacion\""""
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
            ""Apellido"": {
                        ""expression"": {
                            ""columnPath"": ""Surname""
                        }
                    },
            ""TipoDocumento"": {
                        ""expression"": {
                            ""columnPath"": ""BbmContactDocumentType""
                        }
                    },
            ""Cedula"": {
                        ""expression"": {
                            ""columnPath"": ""BbmContactDocumentID""
                        }
                    },
            ""Email"": {
                        ""expression"": {
                            ""columnPath"": ""Email""
                        }
                    },
            ""MobilePhone"": {
                        ""expression"": {
                            ""columnPath"": ""MobilePhone""
                        }
                    },
            ""Type"": {
                        ""expression"": {
                            ""columnPath"": ""Type""
                        }
                    },
            ""SemanaEmbarazo"": {
                        ""expression"": {
                            ""columnPath"": ""BbmContactPregnancyWeeks""
                        }
                    },
            ""fechaCreacion"": {
                        ""expression"": {
                            ""columnPath"": ""CreatedOn""
                        }
                    },
            ""fechaModificacion"": {
                        ""expression"": {
                            ""columnPath"": ""ModifiedOn""
                        }
                    },
            ""BbmContactAceptacionUsoDatosPublicidad"": {
                ""expression"": {
                ""columnPath"": ""BbmContactAceptacionUsoDatosPublicidad""
                        }
                    },
            ""BbmContactFechaAceptacionUsoDatosPublicidad"": {
                ""expression"": {
                    ""columnPath"": ""BbmContactFechaAceptacionUsoDatosPublicidad""
                        }
                    },
            ""BbmContactLandingSource"": {
                ""expression"": {
                    ""columnPath"": ""BbmContactLandingSource""
                        }
                    }

                }
            },
            ""rowCount"": 1,
            ""isPageable"": true,
            ""rowsOffset"": Contador,
            ""ignoreDisplayValues"": true
            }
            ";

            return jsonContactsCRM;
        }

    }
}
