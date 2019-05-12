using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CABBH
{
    class RestClient
    {   

        public enum httpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public string endPoint;
        public httpVerb httpMethod;
        public string token;
        public string username { set; get; }
        public string password { set; get; }
        public string firstname { set; get; }

        public string lastname { set; get; }
        public string email { set; get; }


        public RestClient(string url , httpVerb method)
        {
            endPoint = url;
            httpMethod = method;
            
        }

        public bool check_token_available()
        {


            return false;
        }



        public void token_login()
        {
            //get user from token
        }
        public void SerializeObject<T>(string filename, T obj)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);
            stream.Close();
        }

        public T DeSerializeObject<T>(string filename)
        {
            T objectToBeDeSerialized;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objectToBeDeSerialized = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return objectToBeDeSerialized;
        }

        [Serializable]
        struct Token
        {
            String token;
        }

        public string makeRequestWithToken()
        {   //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();


            token = Properties.Settings.Default.AuthToken;
            if (token != null)
                request.Headers.Add("Authorization", "Token "+ token);
                HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responsestream = response.GetResponseStream())
                {
                    if(responsestream != null)
                    {
                        using (StreamReader reader = new StreamReader(responsestream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                strResponseValue = "{\"errorMessages\":[\"" + e.Message.ToString();
            }
            finally
            {
                if(response!= null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;

        }//make request
        public string RegisterRequest()
        {   //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();

            //add parameters
            string data = "username=" + username +
                "&password=" + password +
                "&first_name=" + firstname
                +"&last_name="+lastname
                + "&password=" + password
                + "&email=" + email; //replace <value>
            byte[] dataStream = Encoding.UTF8.GetBytes(data);


              request.ContentType = "application/x-www-form-urlencoded";
              request.ContentLength = dataStream.Length;  
              Stream newStream = request.GetRequestStream();
              // Send the data.
               newStream.Write(dataStream,0,dataStream.Length);
               newStream.Close();
                  

            
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responsestream = response.GetResponseStream())
                {
                    if (responsestream != null)
                    {
                        using (StreamReader reader = new StreamReader(responsestream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                strResponseValue = "User name already exists";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;

        }//make request

        public string LoginRequest()
        {   //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();

            //add parameters
            string data = "username=" + username + "&password=" + password; //replace <value>
            byte[] dataStream = Encoding.UTF8.GetBytes(data);


            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataStream.Length;
            Stream newStream = request.GetRequestStream();
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();



            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responsestream = response.GetResponseStream())
                {
                    if (responsestream != null)
                    {
                        using (StreamReader reader = new StreamReader(responsestream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                strResponseValue = "{\"errorMessages\":[\"" + e.Message.ToString();
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return strResponseValue;

        }//make request



    }
}
