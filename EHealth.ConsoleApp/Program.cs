using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EHealth.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //login
            LoginRequest body = new()
            {
                UserName = "Mihailo",
                Password = "Imper1us!",
            };
            const string loginUri = "https://localhost:44328/auth/login";
            HttpClient client = new();
            client.Timeout = TimeSpan.FromSeconds(1e4);
            JsonContent content = JsonContent.Create(body);

            var response = await client.PostAsync(loginUri, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("All fucked");
                return;
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var token = JsonDocument.Parse(jsonString).RootElement.GetProperty("token").GetString()!;
            Console.WriteLine(token);

            client.DefaultRequestHeaders.Authorization = new("Bearer", token);
            const string doctorsUri = "https://localhost:44328/api/get-doctors";

            response = await client.GetAsync(doctorsUri);

            Console.WriteLine(response);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("All fucked");
                return;
            }
        }
    }

    class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
