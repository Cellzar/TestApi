using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestLogic
{
    public class Service
    {
        public Service()
        {
        }

        public async Task<string> getAutors(string config)
        {
            var result = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Fiddler");
                    using (HttpResponseMessage response = await client.GetAsync(config + "//Authors"))
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<string> getBooks(string config)
        {
            var result = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Fiddler");
                    using (HttpResponseMessage response = await client.GetAsync(config + "//Books"))
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<string> getUsers(string config)
        {
            var result = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Fiddler");
                    using (HttpResponseMessage response = await client.GetAsync(config + "//Users"))
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
