using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace IntegracionCRM.ApiRest
{
    class DBApi
    {

        public dynamic PostAut(string url, string json, string autorizacion = null)
        {
            try
            {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
           // request.AddHeader("api_key", "a52375e9-5959-47f0-96d5-f9e6de8fd255");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

                dynamic galleta0 = response.Cookies[0].Name;
                dynamic galleta0v = response.Cookies[0].Value;

                dynamic galleta1 = response.Cookies[1].Name;
                dynamic galleta1v = response.Cookies[1].Value;

                dynamic galleta2 = response.Cookies[2].Name;
                dynamic galleta2v = response.Cookies[2].Value;

                dynamic galleta3 = response.Cookies[3].Name;
                dynamic galleta3v = response.Cookies[3].Value;

                string galleta = galleta0+"="+galleta0v+";"+galleta1+"="+galleta1v+";"+galleta2+"="+galleta2v+";"+galleta3+"="+galleta3v;

                dynamic exito = response.IsSuccessful;
            dynamic estatus = response.StatusDescription;
            dynamic error = response.ErrorMessage;


                if (exito == true || estatus == "OK")
               {
               return (galleta2v, galleta);
                    
                }
            else
            {
                return error;
            }

              }
               catch (Exception ex)
               {
                   Console.WriteLine("Error desde POST:" + ex);
                   return null;
               }
        }



        public dynamic PostInsert(string url, string json, string key, string header, string autorizacion = null)
        {
            //try
            //{
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("BPMCSRF", key);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", header);

            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            dynamic dat = response.IsSuccessful;
            dynamic dat2 = response.StatusDescription;
            dynamic dat22 = response.Content;

            if (dat == true || dat2 == "OK")
            {

                dynamic datos = JsonConvert.DeserializeObject(response.Content);
                
                return datos;
            }
            else
            {
                dynamic datos = "revisar conexion o json";
                return datos;
            }


            /*   }
               catch (Exception ex)
               {
                   Console.WriteLine("Error desde POST:" + ex);
                   return null;
               }*/
        }

    }
}
