using IntegracionCRM.ApiRest;
using System;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace IntegracionCRM
{
    public class Program
    {
        static DBApi dBApi = new DBApi();
        static contacto cto = new contacto();
        static producto produ = new producto();
        static consultaContacto consContact = new consultaContacto();
        static invoice invo = new invoice();
        static consultaProducto consItem = new consultaProducto();
        static invoiceProduct invProdu = new invoiceProduct();
        static suscripcionMam mam = new suscripcionMam();
                static string CookieAut = "";
        static string HeaderAut = "";
        static void Main(string[] args)
        {
            try
            {
                 Console.WriteLine("AUTENTICANDO...");
                 autenticacion();
                              

                Console.WriteLine("insertar Socia a tbl central");
                //InsertarSociaTblCentral();///+++  

                 Console.WriteLine("insertar contacto tbl central a crm");
                //InsertarContactoCrm();///+++

             

               
                Console.WriteLine("Actualiza mam socia a tbl central");
                //ActualizaMAMTblCentral();///+++

                 Console.WriteLine("inserta constacto suscrito mam de socia a crm");
                // InsertarContactoSuscritoMAM();  ///+++

                  Console.WriteLine("actualiza estado mam de crm a tblcentral y socia ");
                  //ActualizarEstadoMAM();



                //Console.WriteLine("insertar contacto crm a tbl central");
                InsertarContactoCrmTblCentral();

                Console.WriteLine("insertar contacto tblcentral a socia");
                //InsertarContactoTblCentral_Socia();

                //Console.WriteLine("insertando producto... ");
                //InsertarProducto();//++
                //Console.WriteLine("insertando factura...");
                //InsertarInvoiceCrm();//+++
                //--------------

                // Console.WriteLine("Buscando contacto...");
                //string id = BuscarContactoCrm("1717032138").ToString();

                // Console.WriteLine("Buscando item...");
                // string id = BuscarProductoCrm("001.032695").ToString();



                //insertVentaDetalleCrm("bfdd890e-c10a-4090-8646-770385a8b034", "1c14e919-14f9-4b33-9f10-2254162cfd7e");
            }

            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

         static void autenticacion()
        {
           
            try
            {

                {

                    string jsonAutentication = @"{
""UserName"": ""Supervisor"",
""UserPassword"": ""N@FOB`d?FI.=.gHydCT1""
}";
                                                    
                        dynamic respuesta = dBApi.PostAut("https://bebemundo.creatio.com/ServiceModel/AuthService.svc/Login", jsonAutentication);
                    

                    string res = respuesta.Item1;
                    string res2 = respuesta.Item2;
                    
                    CookieAut = res;
                    HeaderAut = res2;
                    
                    Console.WriteLine(res,res2);
                                     
                    
                }
            }
            catch (SqlException ex)
            {
                string error = ex.ToString();
                //SendMail(error);
                Console.WriteLine(ex);
            }
                        
        }

        private static void InsertarProducto() 
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=InventarioSQL;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_crm_productos", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                   // StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        string nombreItem = dt.Rows[i]["Descripcion_BB"].ToString();
                        string codigoItem = dt.Rows[i]["Codigo_Item"].ToString();
                        string idItem = dt.Rows[i]["id_item"].ToString();
                        string codigoBarras = dt.Rows[i]["Codigo_Barras"].ToString();
                        string division = dt.Rows[i]["division"].ToString();
                        string subdivision = dt.Rows[i]["subdivision"].ToString();
                        string departamento = dt.Rows[i]["departamento"].ToString();
                        string seccion = dt.Rows[i]["seccion"].ToString();
                        string precio = dt.Rows[i]["price"].ToString();

                        dynamic val = produ.product();
                        string json = val;

                        json = json.Replace("NombreItem", nombreItem);
                        json = json.Replace("CodigoItem", codigoItem);
                        json = json.Replace("IdItem", idItem);
                        json = json.Replace("CodigoBarras", codigoBarras);
                        json = json.Replace("IdDivision", division);
                        json = json.Replace("IdSubdivision", subdivision);
                        json = json.Replace("IdDepartamento", departamento);
                        json = json.Replace("IdSeccion", seccion);
                        json = json.Replace("Precio", precio);

                        dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                        string res = respuesta.ToString();
                        Console.WriteLine("Respuesta WS:" + res+ idItem);

                        string update = "update item set Enviado_CRM='S' where id_item ='" + idItem + "'";
                        SqlCommand cmd2 = new SqlCommand(update, con);
                        cmd2.ExecuteNonQuery();

                    }

                }
            }
            catch (SqlException ex)
            {
                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }
        }

      
        public static string BuscarContactoCrm(string cedula) 
        {
             
                dynamic val = consContact.ConsultContact();
                string json = val;
                json = json.Replace("CEDULA", cedula);
               // Console.WriteLine(val);
                dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/SelectQuery", json, CookieAut, HeaderAut);
                var res = respuesta.ToString();
                dynamic idContact = respuesta.rows;
                JObject jo = JObject.Parse(res);

            JToken flag = jo["rowsAffected"].ToString();

            string comp = flag.ToString();

            string ce = "0";

            if (comp == ce)                  
            {
                string sr = "sin registro";
                return sr; 
            }
            else {
                JToken memberName = jo["rows"].First["Id"];
                //Console.WriteLine(memberName);
                return memberName.ToString();
            }

                
            
        }

        public static string BuscarProductoCrm(string codItem)
        {
            dynamic val = consItem.ConsultItem();
            string json = val;
            json = json.Replace("CODITEM", codItem).Replace("\\","-");
           // Console.WriteLine(val);
            dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/SelectQuery", json, CookieAut, HeaderAut);
            var res = respuesta.ToString();
            dynamic idContact = respuesta.rows;
            JObject jo = JObject.Parse(res);

            JToken flag = jo["rowsAffected"].ToString();

            string comp = flag.ToString();

            string ce = "0";

            if (comp==ce)
            {
                string sr = "sin registro";
                return sr;
            }
            else
            {
                JToken memberName = jo["rows"].First["Id"];
                //Console.WriteLine(memberName);
                return memberName.ToString();
            }


            
        }

        // INSERTA LOS CONTACTOS DE CRM A TABLA CENTRAL
        private static void InsertarContactoCrmTblCentral()
        {
                        
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {
                    con.Open();

                    var  fechmax="";
                    //SqlCommand cmd = new SqlCommand("  select max(fecha_creacion_crm) as FechMax FROM Contacto", con);
                    SqlCommand cmd = new SqlCommand(" select convert(varchar, max(fecha_creacion_crm),20) as FechMax FROM Contacto", con);
                    cmd.ExecuteNonQuery();

                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                         fechmax = row["FechMax"].ToString();
                       
                    }

                    Console.WriteLine(fechmax);
                    dynamic val = cto.contactsQuantity();
                    string json = val;
                    json = json.Replace("FechaCreacion", fechmax);
                   
                    dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/SelectQuery", json, CookieAut, HeaderAut);
                    string res = respuesta.ToString();
                    JObject jo = JObject.Parse(res);
                    JToken memberName = jo["rows"].First["IdCOUNT"];
                    int cantidad = memberName.Value<Int32>()-1;
                    Console.WriteLine(memberName);

                    for (int i = 0; i <= cantidad; i++)
                    { 
                    
                    
                    dynamic val2 = cto.recuperaContactosCRM();
                    string json2 = val2;
                    json2 = json2.Replace("FechaCreacion", fechmax).Replace("Contador", Convert.ToString(i));

                    dynamic respuesta2 = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/SelectQuery", json2, CookieAut, HeaderAut);
                    string res2 = respuesta2.ToString();
                    JObject jocrm = JObject.Parse(res2);

                        JToken flag = jo["rowsAffected"].ToString();
                        string comp = flag.ToString();
                        string ce = "0";
                        if(comp==ce)
                        { 
                            break; 
                        }
                        else { 
                        
                        

                    JToken memberNombre = jocrm["rows"].First["Name"];
                    JToken memberAp = jocrm["rows"].First["Apellido"];
                    JToken memberTipoDoc = jocrm["rows"].First["TipoDocumento"]["value"];
                    JToken memberCedula = jocrm["rows"].First["Cedula"];
                    JToken memberEmail = jocrm["rows"].First["Email"];
                    JToken memberMobile = jocrm["rows"].First["MobilePhone"];
                    JToken memberType = jocrm["rows"].First["Type"]["value"];
                    JToken memberSemana = jocrm["rows"].First["SemanaEmbarazo"];

                     JToken memberfechaCreacion  = jocrm["rows"].First["fechaCreacion"];
                     JToken memberfechaModificacion = jocrm["rows"].First["fechaModificacion"];

                     JToken AceptacionUsoDatosPublicidad = jocrm["rows"].First["BbmContactAceptacionUsoDatosPublicidad"];
                     JToken FechaAceptacionUsoDatosPublicidad = jocrm["rows"].First["BbmContactFechaAceptacionUsoDatosPublicidad"];

                            string landing;

                            JToken LandingOrigen = jocrm["rows"].First["BbmContactLandingSource"];

                            if (LandingOrigen.ToString() == "")
                            {
                              landing= "";
                            }
                            else
                            {
                                JToken LandingOrigen2 = jocrm["rows"].First["BbmContactLandingSource"]["value"];
                                 landing = LandingOrigen2.ToString();
                            }


                            switch (AceptacionUsoDatosPublicidad.ToString()) 
                            {
                                case "False":
                                    AceptacionUsoDatosPublicidad = "N";
                                    break;
                                case "True":
                                    AceptacionUsoDatosPublicidad = "S";
                                    break;
                            }



                            switch (memberTipoDoc.ToString()) 
                        {

                            case "b4ed885e-a59d-41d5-9f73-2951f25b52fc":
                                memberTipoDoc = memberTipoDoc.ToString().Replace("b4ed885e-a59d-41d5-9f73-2951f25b52fc","1");
                            break;

                            case "07ce1595-d999-4b2d-8346-e29b1aef0280":
                                memberTipoDoc = memberTipoDoc.ToString().Replace("07ce1595-d999-4b2d-8346-e29b1aef0280", "2");
                                break;

                            case "30c21fce-48a4-45b7-b4b9-64907e6a7f95":
                                memberTipoDoc = memberTipoDoc.ToString().Replace("30c21fce-48a4-45b7-b4b9-64907e6a7f95", "3");
                                break;

                            case "e7aef944-c2e3-4717-b380-b79ccf776000":
                                memberTipoDoc = memberTipoDoc.ToString().Replace("e7aef944-c2e3-4717-b380-b79ccf776000", "4");
                                break;

                            case "7c7f4366-8daf-4215-a867-16c0e192f24d":
                                memberTipoDoc = memberTipoDoc.ToString().Replace("7c7f4366-8daf-4215-a867-16c0e192f24d", "5");
                                break;

                        }

                        switch (memberType.ToString())
                        {

                            case "342e8254-cac0-4ddc-b4f1-d792ca061cd6":
                                memberType = memberType.ToString().Replace("342e8254-cac0-4ddc-b4f1-d792ca061cd6", "1");
                                break;

                            case "c450684a-c225-4118-aa27-c4b9aa381f43":
                                memberType = memberType.ToString().Replace("c450684a-c225-4118-aa27-c4b9aa381f43", "2");
                                break;

                            case "dd905b7f-b902-43a6-bbf2-f3ef210277db":
                                memberType = memberType.ToString().Replace("dd905b7f-b902-43a6-bbf2-f3ef210277db", "3");
                                break;

                            case "00783ef6-f36b-1410-a883-16d83cab0980":
                                memberType = memberType.ToString().Replace("00783ef6-f36b-1410-a883-16d83cab0980", "4");
                                break;

                            case "60733efc-f36b-1410-a883-16d83cab0980":
                                memberType = memberType.ToString().Replace("60733efc-f36b-1410-a883-16d83cab0980", "5");
                                break;

                            case "ac278ef3-e63f-48d9-ba34-7c52e92fecfe":
                                memberType = memberType.ToString().Replace("ac278ef3-e63f-48d9-ba34-7c52e92fecfe", "6");
                                break;

                            case "806732ee-f36b-1410-a883-16d83cab0980":
                                memberType = memberType.ToString().Replace("806732ee-f36b-1410-a883-16d83cab0980", "7");
                                break;


                        }


                        SqlCommand insert = new SqlCommand("spInsertarContactoCrmAtblCentral", con);
                         insert.CommandTimeout = 0;
                        insert.CommandType = CommandType.StoredProcedure;

                    // for
                    try
                    {
                         
                            insert.Parameters.Clear();
                            insert.Parameters.AddWithValue("@Nombre", memberNombre.ToString());
                            insert.Parameters.AddWithValue("@Apellido", memberAp.ToString());
                            insert.Parameters.AddWithValue("@id_Tipo_Documento", memberTipoDoc.ToString());
                            insert.Parameters.AddWithValue("@Numero_Documento", memberCedula.ToString());
                            insert.Parameters.AddWithValue("@Mail", memberEmail.ToString());
                            insert.Parameters.AddWithValue("@Celular", memberMobile.ToString());
                            insert.Parameters.AddWithValue("@id_Tipo_Contacto", memberType.ToString());
                            insert.Parameters.AddWithValue("@Recibido_CRM", "S");
                            insert.Parameters.AddWithValue("@Enviado_CRM", "S");
                            insert.Parameters.AddWithValue("@Recibido_Mysql", "");
                            insert.Parameters.AddWithValue("@Enviado_Mysql", "N");
                            insert.Parameters.AddWithValue("@Actualizado_CRM", "");
                            insert.Parameters.AddWithValue("@Semana_Embarazo", memberSemana.ToString());
                            insert.Parameters.AddWithValue("@Id_Contacto_CRM", "");
                            insert.Parameters.AddWithValue("@Origen_Registro", "CRM");
                            insert.Parameters.AddWithValue("@Tienda", "CRM");

                            insert.Parameters.AddWithValue("@Fecha_Creacion_Crm", memberfechaCreacion.ToString());
                            insert.Parameters.AddWithValue("@Fecha_Modificacion_Crm", memberfechaModificacion.ToString());
                            
                            insert.Parameters.AddWithValue("@AceptacionUsoDatosPublicidad", AceptacionUsoDatosPublicidad.ToString());
                            insert.Parameters.AddWithValue("@FechaAceptacionUsoDatosPublicidad", FechaAceptacionUsoDatosPublicidad.ToString());
                            insert.Parameters.AddWithValue("@LandingOrigen", landing.ToString());



                                insert.ExecuteNonQuery();
                            // Console.WriteLine(res2);
                            //    string update = "update openquery(BABYSCLUB, 'Select Cedula_socia,ENVIADO_TCENTRAL from babysclub.socia where Cedula_socia=''" + cedula + "'''" + ") set ENVIADO_TCENTRAL='S' ";
                            


                        }

                        catch (SqlException ex)
                        {
                            Console.WriteLine("No se pudo insertar porque el contacto ya existe");
                        }
                       
                    }
                    }

                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }
        }



        //ENVIA LOS REGISTROS DE TABLA CENTRAL A LA TABLA SOCIA DE BABYS MYSQL
        private static void InsertarContactoTblCentral_Socia()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.EnviarContactotblCentral_tblsocia", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    //  StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        try
                        {
                            
                            string nombre = dt.Rows[i]["Nombre"].ToString();
                            string apellido = dt.Rows[i]["Apellido"].ToString();
                            string numero_documento = dt.Rows[i]["Numero_Documento"].ToString();
                            string mail = dt.Rows[i]["Mail"].ToString();
                            string celular = dt.Rows[i]["Celular"].ToString();
                            string semana_embarazo = dt.Rows[i]["Semana_Embarazo"].ToString();
                            string tipo_contacto_crm = dt.Rows[i]["Tipo_Contacto_Crm"].ToString();
                            string tipo_documento = dt.Rows[i]["Tipo_Documento"].ToString();
                            string origen=dt.Rows[i]["Origen_Registro"].ToString();
                            string Fecha_Creacion_Crm = dt.Rows[i]["Fecha_Creacion_Crm"].ToString();
                            string id_tipo_cliente_socia = dt.Rows[i]["id_tipo_cliente_socia"].ToString();

                            string Aceptacion_Envio_Publicidad = dt.Rows[i]["Aceptacion_Envio_Publicidad"].ToString();
                            string Fecha_Aceptacion_Envio_Publicidad = dt.Rows[i]["Fecha_Aceptacion_Envio_Publicidad"].ToString();
                            string Landing_Origen = dt.Rows[i]["Landing_Origen"].ToString();

                            string estado ="Activa";

                            string exist = "select *from openquery(BABYSCLUB,'select trim(s.CEDULA_SOCIA) as Numero_Documento from babysclub.socia s where s.CEDULA_SOCIA=''" + numero_documento + "''')";
                            SqlCommand cmdExist = new SqlCommand(exist, con);
                            object result = cmdExist.ExecuteScalar();
                            if (result != null)
                            {
                                // La consulta devolvió al menos un registro
                                string updt = "update contacto set Enviado_Mysql='S' where Numero_Documento ='" + numero_documento + "'";
                                                               
                                SqlCommand cmde = new SqlCommand(updt, con);
                                cmde.ExecuteNonQuery();

                            }
                            else
                            {
                                // La consulta no devolvió ningún registro
                            


                            string insertSocia = "INSERT OPENQUERY (BABYSCLUB, 'SELECT TIPO_identificacion,cedula_socia,codigo_categoria," +
                                "nombre_socia,apellido_socia,fechanacimiento_socia,estado_socia,usuario_app,clave_app,TIPO_CLIENTE,TIPO_CRM,fechasuscripcion_socia,fechaultimaactivacion_socia,Aceptacion_Envio_Publicidad,Fecha_Aceptacion_Envio_Publicidad,LANDING_ORIGEN,SEMANAEMBARAZO_SOCIA FROM babysclub.socia')" +
                                "VALUES('"+tipo_documento+"', '"+numero_documento+"',1,'"+nombre+"', '"+apellido+ "', getdate(), '"+estado+"','"+numero_documento+"','"+numero_documento+"','"+id_tipo_cliente_socia+"','S','"+Fecha_Creacion_Crm+"','"+Fecha_Creacion_Crm+"','"+Aceptacion_Envio_Publicidad+"','"+Fecha_Aceptacion_Envio_Publicidad+"','"+Landing_Origen+ "','" + semana_embarazo + "')";



                            string insertEmail = "INSERT OPENQUERY (BABYSCLUB, 'SELECT cedula_socia," +
                                "DESCRIPCION_DIRECCION FROM babysclub.email')" +
                                "VALUES ( '" + numero_documento + "', '" + mail + "')";
                                                      

                            string insertTelefono = "INSERT OPENQUERY (BABYSCLUB, 'SELECT cedula_socia," +
                              "NUMERO_TELEFONOSOCIA FROM babysclub.telefono_socia')" +
                              "VALUES ( '" + numero_documento + "', '" + celular + "')";



                            string update = "update contacto set Enviado_Mysql='S' where Numero_Documento ='" + numero_documento + "'";

                            SqlCommand cmd2 = new SqlCommand(insertSocia, con);
                            cmd2.ExecuteNonQuery();

                            SqlCommand cmd5 = new SqlCommand(update, con);
                            cmd5.ExecuteNonQuery();

                            SqlCommand cmd4 = new SqlCommand(insertTelefono, con);
                            cmd4.ExecuteNonQuery();


                            SqlCommand cmd3 = new SqlCommand(insertEmail, con);
                            cmd3.ExecuteNonQuery();

                            }

                        }

                        catch (SqlException ex)
                        {
                            string error = ex.ToString();
                            // SendMail(error);
                            Console.WriteLine(ex);
                        }

                    }


                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }


        }
        //envia el contacto de tbl central a crm
        private static void InsertarContactoCrm()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {
               
                     con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_crm_contacto", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                                                            
                  //  StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        string nombre = dt.Rows[i]["Nombre"].ToString();
                        string apellido = dt.Rows[i]["Apellido"].ToString();
                        string mail = dt.Rows[i]["Mail"].ToString();
                        string celular = dt.Rows[i]["Celular"].ToString();
                        string tipo_cliente = dt.Rows[i]["id_Tipo_Contacto"].ToString();
                        string tipo_documento = dt.Rows[i]["id_Tipo_Documento"].ToString();
                        string numero_documento = dt.Rows[i]["Numero_Documento"].ToString();

                        string Semana_Embarazo = dt.Rows[i]["Semana_Embarazo"].ToString();
                        string Aceptacion_Uso_Datos_Tienda = dt.Rows[i]["Aceptacion_Uso_Datos_Tienda"].ToString();
                        string Fecha_Aceptacion_Uso_Datos_Tienda = dt.Rows[i]["Fecha_Aceptacion_Uso_Datos_Tienda"].ToString();



                        if (Fecha_Aceptacion_Uso_Datos_Tienda =="")
                        {
                            dynamic val = cto.contactSF();

                            string json = val;

                            json = json.Replace("nombre2", nombre);
                            json = json.Replace("apellido2", apellido);
                            json = json.Replace("mail2", mail);
                            json = json.Replace("telefono2", celular);
                            json = json.Replace("tipoContacto2", tipo_cliente);
                            json = json.Replace("tipoDocumento2", tipo_documento);
                            json = json.Replace("documento2", numero_documento);

                            json = json.Replace("semana", Semana_Embarazo);
                            json = json.Replace("bool", Aceptacion_Uso_Datos_Tienda);
                            json = json.Replace("date", Fecha_Aceptacion_Uso_Datos_Tienda);

                            dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                            string res = respuesta.ToString();


                            if (res == "revisar conexion o json")
                            {
                                Console.WriteLine("Respuesta WS:" + res);
                            }
                            else
                            {
                                Console.WriteLine("Respuesta WS:" + res);
                                string update = "update contacto set Enviado_CRM='S' where Numero_Documento ='" + numero_documento + "'";
                                SqlCommand cmd2 = new SqlCommand(update, con);
                                cmd2.ExecuteNonQuery();
                            }

                        }
                        else { 
                        dynamic val = cto.contact();

                            string json = val;

                            json = json.Replace("nombre2", nombre);
                            json = json.Replace("apellido2", apellido);
                            json = json.Replace("mail2", mail);
                            json = json.Replace("telefono2", celular);
                            json = json.Replace("tipoContacto2", tipo_cliente);
                            json = json.Replace("tipoDocumento2", tipo_documento);
                            json = json.Replace("documento2", numero_documento);

                            json = json.Replace("semana", Semana_Embarazo);
                            json = json.Replace("bool", Aceptacion_Uso_Datos_Tienda);
                            json = json.Replace("date", Fecha_Aceptacion_Uso_Datos_Tienda);

                            dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                            string res = respuesta.ToString();


                            if (res == "revisar conexion o json")
                            {
                                Console.WriteLine("Respuesta WS:" + res);
                            }
                            else
                            {
                                Console.WriteLine("Respuesta WS:" + res);
                                string update = "update contacto set Enviado_CRM='S' where Numero_Documento ='" + numero_documento + "'";
                                SqlCommand cmd2 = new SqlCommand(update, con);
                                cmd2.ExecuteNonQuery();
                            }
                        }


                       

                }
                        
                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
               // SendMail(error);
                Console.WriteLine(ex);

            }

        }


        // envia los datos de MAM tbl central a MAM CRM
        private static void InsertarContactoSuscritoMAM()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_crm_mam", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    //  StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        try
                        {
                        string numero_documento = dt.Rows[i]["Numero_Documento"].ToString();
                        string Fecha_Suscrip_MAM = dt.Rows[i]["Fecha_Suscrip_MAM"].ToString();
                        string id_Status_Mam_Crm = dt.Rows[i]["id_Status_Mam_Crm"].ToString();
                        string id_Tipo_Suscrip_Mam_Crm = dt.Rows[i]["id_Tipo_Suscrip_Mam_Crm"].ToString();
                                                    
                        string idcrm = BuscarContactoCrm(numero_documento).ToString();
                                           
                        dynamic val = mam.insertMam();
                        string json = val;

                        json = json.Replace("idcrm2", idcrm);
                        json = json.Replace("Fecha_Suscrip_MAM2", Fecha_Suscrip_MAM);
                        json = json.Replace("id_Status_Mam_Crm2", id_Status_Mam_Crm);
                        json = json.Replace("id_Tipo_Suscrip_Mam_Crm2", id_Tipo_Suscrip_Mam_Crm);
                       
                        dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                        string res = respuesta.ToString();
                        Console.WriteLine("Respuesta WS:" + res);

                        string update = "update contacto set Enviado_Mam_CRM='S' where Numero_Documento ='" + numero_documento + "'";
                        SqlCommand cmd2 = new SqlCommand(update, con);
                        cmd2.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                        Console.WriteLine("No Se encontro numero de documento");
                        }

        }

                }
            }
            catch (SqlException ex)
            {
                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);
            }

        }

        //actualiza el estado mam Activo o Inactivo que viene desde CRM en tbla central y tbl socia 
        private static void ActualizarEstadoMAM()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.ActualizaEstadoMam", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    //  StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        try
                        {
                            string numero_documento = dt.Rows[i]["Numero_Documento"].ToString();
                       
                            dynamic val = mam.BuscarEstadoMam();
                            string json = val;

                            json = json.Replace("CEDULA", numero_documento);
                         
                            dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/SelectQuery", json, CookieAut, HeaderAut);
                           
                            var res = respuesta.ToString();

                            dynamic idContact = respuesta.rows;

                            JObject jo = JObject.Parse(res);

                            JToken memberName = jo["rows"].First["BbmContactMAMMenbership"];

                            if (memberName.ToString() == "True") 
                            {
                                string update = "update contacto set Id_Status_Mam=1, Es_MAM='S' where Numero_Documento ='" + numero_documento + "'";
                                SqlCommand cmd2 = new SqlCommand(update, con);
                                cmd2.ExecuteNonQuery();

                                string updateMam = "update openquery(BABYSCLUB, 'Select Cedula_socia,SUSCRIP_MAM_ACTIVO from babysclub.socia where Cedula_socia=''" + numero_documento + "'''" + ") set SUSCRIP_MAM_ACTIVO='S' ";
                                SqlCommand cmd3 = new SqlCommand(updateMam, con);
                                cmd3.ExecuteNonQuery();
                            }
                           
                            else 
                            {
                                string update = "update contacto set Id_Status_Mam=2,Es_MAM='N' where Numero_Documento ='" + numero_documento + "'";
                                SqlCommand cmd2 = new SqlCommand(update, con);
                                cmd2.ExecuteNonQuery();

                                string updateMam = "update openquery(BABYSCLUB, 'Select Cedula_socia,SUSCRIP_MAM_ACTIVO from babysclub.socia where Cedula_socia=''" + numero_documento + "'''" + ") set SUSCRIP_MAM_ACTIVO='N' ";
                                SqlCommand cmd3 = new SqlCommand(updateMam, con);
                                cmd3.ExecuteNonQuery();
                            }

                        }

                        catch (SqlException ex)
                        {
                            Console.WriteLine("No Se encontro numero de documento");
                        }

                    }

                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }

        }


        /// para enviar los registos de la tabla socia a tabla central COnTACTO
        private static void InsertarSociaTblCentral()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spSocias_ContactoCentral", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    SqlCommand insert = new SqlCommand("spInsertarContactoSociaAtblCentral", con);    
                    insert.CommandTimeout = 0;
                    insert.CommandType = CommandType.StoredProcedure;

                    //  StringBuilder jsonf = new StringBuilder();

                    foreach(DataRow row in dt.Rows)

                    {
                        try
                        {

                            insert.Parameters.Clear();
                            insert.Parameters.AddWithValue("@Nombre", Convert.ToString(row["Nombre"]));
                            insert.Parameters.AddWithValue("@Apellido", Convert.ToString(row["Apellido"]));
                            insert.Parameters.AddWithValue("@id_Tipo_Documento", Convert.ToString(row["id_Tipo_Documento"]));
                            insert.Parameters.AddWithValue("@Numero_Documento", Convert.ToString(row["Numero_Documento"]));
                            insert.Parameters.AddWithValue("@Mail", Convert.ToString(row["Mail"]));
                            insert.Parameters.AddWithValue("@Celular", Convert.ToString(row["Celular"]));
                            insert.Parameters.AddWithValue("@id_Tipo_Contacto", Convert.ToString(row["id_Tipo_Contacto"]));
                            insert.Parameters.AddWithValue("@Recibido_CRM","N");
                            insert.Parameters.AddWithValue("@Enviado_CRM","N");
                            insert.Parameters.AddWithValue("@Recibido_Mysql", "S");
                            insert.Parameters.AddWithValue("@Enviado_Mysql", "N");
                            insert.Parameters.AddWithValue("@Actualizado_CRM", "N");
                            insert.Parameters.AddWithValue("@Semana_Embarazo", Convert.ToString(row["Semana_Embarazo"]));
                            insert.Parameters.AddWithValue("@Id_Contacto_CRM", "");
                            insert.Parameters.AddWithValue("@Origen_Registro", Convert.ToString(row["Origen_Registro"]));
                            insert.Parameters.AddWithValue("@Tienda", Convert.ToString(row["Tienda"]));
                            
                            insert.Parameters.AddWithValue("@ES_MAM", Convert.ToString(row["Es_MAM"]));
                            insert.Parameters.AddWithValue("@FECHA_SUSCRIP_MAM", Convert.ToString(row["Fecha_Suscrip_MAM"]));
                            insert.Parameters.AddWithValue("@ID_STATUS_MAM", Convert.ToString(row["Id_Status_Mam"]));
                            insert.Parameters.AddWithValue("@ID_TIPO_SUSCRIP_MAM", Convert.ToString(row["id_Tipo_Suscrip_Mam"]));

                            insert.Parameters.AddWithValue("@Aceptacion_Uso_Datos_Tienda", Convert.ToString(row["ACEPTA_DATOS_TIENDA"]));
                            insert.Parameters.AddWithValue("@Fecha_Aceptacion_Uso_Datos_Tienda", Convert.ToString(row["FECH_ACEPTA_DATOS_TIENDA"]));

                            insert.ExecuteNonQuery();
                                                  
                            string cedula = row["Numero_Documento"].ToString();

                            string MAM = row["Es_MAM"].ToString();
                            

                            if (MAM == "S") 
                            {
                             string updateMam = "update openquery(BABYSCLUB, 'Select Cedula_socia,ENVIADO_MAM_TCENTRAL from babysclub.socia where Cedula_socia=''" + cedula + "'''" + ") set ENVIADO_MAM_TCENTRAL='S' ";
                             SqlCommand cmd2 = new SqlCommand(updateMam, con);
                             cmd2.ExecuteNonQuery();
                            }


                            //string updat = "update item set Enviado_CRM='S' where id_item ='" + cedula + "'";

                            string update = "update openquery(BABYSCLUB, 'Select Cedula_socia,ENVIADO_TCENTRAL from babysclub.socia where Cedula_socia=''" + cedula + "'''"+") set ENVIADO_TCENTRAL='S' ";
                            SqlCommand cmd3 = new SqlCommand(update, con);
                            cmd3.ExecuteNonQuery();


                            string upd = "update contacto set Fecha_Suscrip_MAM=NULL  where Fecha_Suscrip_MAM='1900-01-01 00:00:00.000' ";
                            SqlCommand cmd4 = new SqlCommand(upd, con);
                            cmd4.ExecuteNonQuery();

                            string updf = "update contacto set Fecha_Aceptacion_Uso_Datos_Tienda=NULL  where Fecha_Aceptacion_Uso_Datos_Tienda='1900-01-01 00:00:00.000' ";
                            SqlCommand cmd5 = new SqlCommand(updf, con);
                            cmd5.ExecuteNonQuery();
                        }

                        catch (SqlException ex)
                        {
                            Console.WriteLine("No se pudo insertar porque el contacto ya existe");
                        }
                    }


                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }

        }

        //funcion para actualizar registros mam mysql a mam tabla intermedia ***************
        private static void ActualizaMAMTblCentral()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("spActualizaMAM_TblCentral", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    
                    foreach (DataRow row in dt.Rows)

                    {
                        try
                        {
                                                       
                            string Numero_Documento = row["Numero_Documento"].ToString();
                            string ES_MAM = row["ES_MAM"].ToString();
                            string FECHA_SUSCRIP_MAM = row["FECHA_SUSCRIP_MAM"].ToString();
                            string id_Tipo_Suscrip_Mam = row["id_Tipo_Suscrip_Mam"].ToString();
                            string Id_Status_Mam = row["Id_Status_Mam"].ToString();

                            string update = "update Contacto set  Es_MAM='"+ES_MAM+"' ,Fecha_Suscrip_MAM='"+FECHA_SUSCRIP_MAM+"' ,Id_Status_Mam='"+Id_Status_Mam+"',Id_Tipo_Suscrip_Mam='" +id_Tipo_Suscrip_Mam+"',Enviado_Mam_CRM='N' where Numero_Documento ='"+Numero_Documento+"'";
                            SqlCommand cmd2 = new SqlCommand(update, con);
                            cmd2.ExecuteNonQuery();

                            string updateMam = "update openquery(BABYSCLUB, 'Select Cedula_socia,ENVIADO_MAM_TCENTRAL from babysclub.socia where Cedula_socia=''"+Numero_Documento+"'''"+ ") set ENVIADO_MAM_TCENTRAL='S' ";
                            SqlCommand cmd3 = new SqlCommand(updateMam, con);
                            cmd3.ExecuteNonQuery();
                        }

                        catch (SqlException ex)
                        {
                            Console.WriteLine("No se pudo Actualizar");
                        }
                    }


                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }

        }

        //funcion para enviar a crm venta cabecera 
        private static void InsertarInvoiceCrm()  
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_InsertInvoice", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                  //  StringBuilder jsonf = new StringBuilder();
                    for (
                        int i = 0; i < dt.Rows.Count; i++)

                    {
                        try {                  

                        string cedula = dt.Rows[i]["identificacion"].ToString();
                       
                            string identificacion = BuscarContactoCrm(cedula).ToString();



                        string fecha = dt.Rows[i]["fecha"].ToString();
                        string transaccion = dt.Rows[i]["transaccion"].ToString();
                        string local = dt.Rows[i]["local"].ToString();
                        string caja = dt.Rows[i]["caja"].ToString();
                        string ventaCabecera=dt.Rows[i]["id_venta_cabecera"].ToString();
                        string vc="'"+ventaCabecera+"'";  // tomar variable
                       
                        string tipoTransaccion=dt.Rows[i]["TipoTrans"].ToString();
                        string id_DatosFactura = dt.Rows[i]["id_DatosFactura"].ToString();

                            string json = invo.factura();
                       // string json = val;

                        json = json.Replace("contacto2", identificacion);
                        json = json.Replace("fecha2", fecha).Replace("/","-");
                        json = json.Replace("transaccion2", transaccion);
                        json = json.Replace("tienda2", local);
                        json = json.Replace("caja2", caja);
                        json = json.Replace("tipo2", tipoTransaccion);
                        json = json.Replace("contacto2", id_DatosFactura);

                            dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                        string res = respuesta.ToString();


                            string comp = "revisar conexion o json";

                            if (res == comp) 
                            { 
                                Console.WriteLine("no se inserto vc, no se encontro cliente"); }
                          
                            else { 

                            string idFactura = respuesta.id.ToString();  //tomaR VARIABÑLE 
                        
                            Console.WriteLine("transaccion:" + transaccion);
                                                

                            insertVentaDetalleCrm(idFactura, ventaCabecera);

                            string insert = $"insert into Factura_Enviada (id_venta_cabecera) values({vc})";
                            SqlCommand cmd2 = new SqlCommand(insert, con);
                            cmd2.ExecuteNonQuery();
                            }
                        }

                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }


                }
            }
            catch (SqlException ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine(ex);

            }

        }
       
        //funcion para enviar a crm venta detalle
        public static void insertVentaDetalleCrm(string idFacturaCrm,string vc) 
        {

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=facturacion.bebelandia.com.ec;Initial Catalog=CRM;User ID=inventario;Password=inventa3iobm11$;"))

                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.InsertInvoiceDetalle", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_vc", SqlDbType.VarChar).Value = vc;
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    //  StringBuilder jsonf = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)

                    {
                        try { 
                        string id_venta_cabecera_crm = idFacturaCrm;
                        string codigoItem=dt.Rows[i]["codigo_item"].ToString();

                        string idItem = BuscarProductoCrm(codigoItem).ToString();   // buscamos producto 

                        //string idItem = "a802f388-59aa-4b69-88c3-ecc9078bf27d";
                        string cantidad = dt.Rows[i]["cantidad"].ToString();
                        string pvp = dt.Rows[i]["pvp"].ToString().Replace(",",".");
                        string amount = dt.Rows[i]["amount"].ToString().Replace(",", ".");
                        string taxamount = dt.Rows[i]["taxamount"].ToString().Replace(",", ".");
                        string totalamount = dt.Rows[i]["totalamount"].ToString().Replace(",", ".");

                        //  string vc = "'" + ventaCabecera + "'";
                        string json = invProdu.facturaDetalle();
                        // string json = val;
                   
                        json = json.Replace("id_venta_cabecera_crm2", id_venta_cabecera_crm);
                        json = json.Replace("codigoItem2", idItem);
                        json = json.Replace("cantidad2", cantidad);
                        json = json.Replace("pvp2", pvp);
                        json = json.Replace("monto2", amount);
                        json = json.Replace("impuesto2", taxamount);
                        json = json.Replace("valor2", totalamount);

                        dynamic respuesta = dBApi.PostInsert("https://bebemundo.creatio.com/0/DataService/json/SyncReply/InsertQuery", json, CookieAut, HeaderAut);
                        string res = respuesta.ToString();
                       // string idFactura = respuesta.id.ToString();  //tomaR VARIABÑLE 
                        Console.WriteLine("venta detalle" + id_venta_cabecera_crm);

                        //    string insert = $"insert into Factura_Enviada (id_venta_cabecera) values({vc})";
                          //  SqlCommand cmd2 = new SqlCommand(insert, con);
                           // cmd2.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {

                         Console.WriteLine(ex);

                        }
                    }


                }
            }
            catch (Exception ex)
            {

                string error = ex.ToString();
                // SendMail(error);
                Console.WriteLine("error vd");

            }

        }

    }
}
