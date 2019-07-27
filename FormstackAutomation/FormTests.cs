using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;

namespace FormstackAutomation
{
    public class FormTests
    {
        public int success = 0;
        public int fail = 0;

        public void GetAllFormsWithoutAuthToken()
        {
            // Define the URL
            var client = new HttpClient();

            // Send the request
            var response = client.GetAsync("https://caitlinleonard.formstack.com/api/v2/form").Result;

            // Get the response code and data in JSON format
            // Check if the response is OK (401- Unauthorized) -> Test passed, if reponse is something else -> test failed
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("GetAllFormsWithoutAuthToken PASSED");
                success++;
                
            }
            else
            {
                Console.WriteLine("GetAllFormsWithoutAuthToken FAILED  with status code: " + response.StatusCode);
                fail++;
            }


        }

        public void GetAllFormsWithAuthToken()
        {
            // Define the URL
            var client = new HttpClient();

            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");

            // Send the request
            var response = client.GetAsync("https://caitlinleonard.formstack.com/api/v2/form").Result;

            // Get the response code and data in JSON format
            // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("GetAllFormsWithAuthToken PASSED");
                success++;
            }
            else
            {
                Console.WriteLine("GetAllFormsWithAuthToken FAILED  with status code: " + response.StatusCode);
                fail++;
            }


        }

        //Get a specific form known to exist
        public async void GetSpecificKnownForm()
        {
            // Define the URL
            var client = new HttpClient();

            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");

            // Send the request
            int formId = 3527609;
            var response = client.GetAsync("https://caitlinleonard.formstack.com/api/v2/form/" + formId).Result;

            // Get the response code and data in JSON format
            // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(json);

                int id = Convert.ToInt32(jsonResponse.SelectToken("id"));

                if (id == formId)
                {
                    Console.WriteLine("GetSpecificKnownForm PASSED");
                    success++;
                }
                else
                {
                    Console.WriteLine("GetSpecificKnownForm FAILED  Different ID");
                    fail++;
                    return;
                }

            }
            else
            {
                Console.WriteLine("Failed with status code: " + response.StatusCode);
                fail++;
            }

        }

        //Test a range of ids that may or may not exist
        public void GetFormByIdRange()
        {
            // Define the URL
            var client = new HttpClient();
            var ids = Enumerable.Range(0, 100);

            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");


            foreach (var id in ids)
            {
                var response = client.GetAsync("https://caitlinleonard.formstack.com/api/v2/form/" + id).Result;

                // Get the response code and data in JSON format
                // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("GetFormByIdRange PASSED. FormID: " + id);
                    success++;
                }
                else
                {
                    Console.WriteLine("GetFormByIdRange FAILED  with status code: " + response.StatusCode + "FormId: " + id);
                    fail++;
                }

            }

        }

        public async void CopyFormById()
        {
            // Define the URL
            var client = new HttpClient();

            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");

            // Initialize the string content for POST call. 
            var content = new StringContent("");

            // Send the request
            int formId = 3527609;

            var response = client.PostAsync("https://caitlinleonard.formstack.com/api/v2/form/" + formId + "/copy", content).Result;

            // Get the response code and data in JSON format
            // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(json);

                int id = Convert.ToInt32(jsonResponse.SelectToken("id"));

                if (id != formId)
                {
                    Console.WriteLine("CopyFormById PASSED  Created Form new id: " + id);
                    success++;
                }
                else
                {
                    Console.WriteLine("CopyFormById FAILED  Same ID");
                    fail++;
                    return;
                }

            }
            else
            {
                Console.WriteLine("CopyFormById FAILED  with status code: " + response.StatusCode);
                fail++;
            }

        }

        public async void DeleteForm()
        {
            // Define the URL
            var client = new HttpClient();

            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");

            // Send the request
            int formId = 3532302;
            var response = client.DeleteAsync("https://caitlinleonard.formstack.com/api/v2/form/" + formId).Result;

            // Get the response code and data in JSON format
            // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(json);

                int id = Convert.ToInt32(jsonResponse.SelectToken("id"));

                if (id == formId)
                {
                    Console.WriteLine("DeleteForm PASSED");
                    success++;
                }
                else
                {
                    Console.WriteLine("DeleteForm FAILED  Different ID");
                    fail++;
                    return;
                }

            }
            else
            {
                Console.WriteLine("DeleteForm FAILED with status code: " + response.StatusCode);
                fail++;
            }
        }

        //Test a range of ids that may or may not exist
        public void DeleteFormIdRange()
        {
            // Define the URL
            var client = new HttpClient();
            var ids = Enumerable.Range(0, 100);

            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");


            foreach (var id in ids)
            {
                var response = client.DeleteAsync("https://caitlinleonard.formstack.com/api/v2/form/" + id).Result;

                // Get the response code and data in JSON format
                // Check if the response is OK (200) -> Test passed, if reponse is not OK (200) -> test failed
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("DeleteFormIdRange PASSED  FormID: " + id);
                    success++;
                }
                else
                {
                    Console.WriteLine("DeleteFormIdRange FAILED  with status code: " + response.StatusCode + "FormId: " + id);
                    fail++;
                }

            }

        }

        public async void CopyFormIdRange()
        {
            // Define the URL
            var client = new HttpClient();
            var ids = Enumerable.Range(0, 100);

            // Initialize the string content for POST call. 



            // Attach the Authorization token to the header Key and Value
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 89951180f1563429c0a3437a14816c2c");

            foreach (var id in ids)
            {
                var content = new StringContent("");
                var response = client.PostAsync("https://caitlinleonard.formstack.com/api/v2/form/" + id + "/copy", content).Result;

                // Get the response

                var json = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(json);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                   
                    Console.WriteLine("CopyFormIdRange PASSED  FormID: " + id + "  was not found");
                    success++;
                }
                else
                {

                    string error = jsonResponse.SelectToken("error").ToString();
                    Console.WriteLine("CopyFormIdRange FAILED  FormID: " +id + "  " + error + "Failed with status code: " + response.StatusCode);
                    fail++;
                }


            }
        }

        



    }
}

        
    

