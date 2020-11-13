using KeyvaultWindowsFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyvaultWindowsFormsApp.Services
{
    public class CallWebApi
    {
        private string _baseadres = "https://localhost:44399/api/";
        static HttpClient client = new HttpClient();
        public CallWebApi()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_baseadres);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public bool Login(LoginViewModel model)
        {
            try
            {
                var postTask = client.PostAsJsonAsync<LoginViewModel>("Account/Login", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseTask2 = client.GetAsync($"Account/GetUserID/{model.Email}");
                    responseTask2.Wait();
                    var result2 = responseTask2.Result;
                    var read2 = result2.Content.ReadAsAsync<string>();
                    read2.Wait();
                    StaticGlobalVariables.UserID = read2.Result;
                }
                return result.IsSuccessStatusCode;
            }
            catch (Exception e )
            {

                throw e;
            }
        }
        public IEnumerable<KeyViewModel> GetKeys(string userid)
        {
            IEnumerable<KeyViewModel> keys = null;
            var responseTask = client.GetAsync($"Key/GetKeys/{userid}");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<KeyViewModel>>();
                readTask.Wait();
                keys = readTask.Result;
            }
            else
            {
                keys = System.Linq.Enumerable.Empty<KeyViewModel>();
            }
            return keys;
        }
        public bool DeleteRow(string id)
        {
            try
            {
                var responseTask = client.DeleteAsync($"Key/DeleteKey/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public KeyViewModel GetKey(string id)
        {
            try
            {
                KeyViewModel key = new KeyViewModel();
                var responseTask = client.GetAsync($"Key/GetKey/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<KeyViewModel>();
                    readTask.Wait();

                    key = readTask.Result;
                }
                else
                {

                    key = null;
                }
                return key;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public bool UpdateKey(KeyViewModel key)
        {
            try
            {
                var postTask = client.PostAsJsonAsync<KeyViewModel>("Key/UpdateKey", key);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CreateKey(KeyViewModel key)
        {
            try
            {
                var postTask = client.PostAsJsonAsync<KeyViewModel>("Key/CreateAsync", key);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<UserListViewModel> GetUsers()
        {
            IEnumerable<UserListViewModel> users = null;
            var responseTask = client.GetAsync("Key/GetUsers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                 var readTask = result.Content.ReadAsAsync<IList<UserListViewModel>>();
                 readTask.Wait();
                 users = readTask.Result;
            }
            else
            { 
               users = null;
            }
            return users;
        }
        public bool Register(RegisterBindingModel model)
        {
            try
            {
                var postTask = client.PostAsJsonAsync<RegisterBindingModel>("Account/Register", model);
                postTask.Wait();

                var result = postTask.Result;
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public UserLogViewModel GetLastLogin()
        {
            try
            {
                var postTask = client.GetAsync($"Key/GetUserLog/{StaticGlobalVariables.UserID}");
                postTask.Wait();

                var result = postTask.Result;
                var read2 = result.Content.ReadAsAsync<UserLogViewModel>();
                read2.Wait();

                return read2.Result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
