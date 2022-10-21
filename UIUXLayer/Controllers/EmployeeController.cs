using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using UIUXLayer.Models;


namespace UIUXLayer.Controllers
{
    public class EmployeeController : Controller
    {
        
        public async Task<IActionResult> viewEmployee()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            List<ModelClass>? employee = new List<ModelClass>();
            
            HttpResponseMessage res = await client.GetAsync("api/Employee");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
            }
            return View(employee);

        }
        public async Task<IActionResult> Details(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            ModelClass? employee = new ModelClass();

            HttpResponseMessage res = await client.GetAsync($"api/Employee/get/{username}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<ModelClass>(result);
            }
            return View(employee);

        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(ModelClass emp)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            var postTask = client.PostAsJsonAsync<ModelClass>("api/Employee/create", emp);
            postTask.Wait();
            var Result = postTask.Result;
            if(Result.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewEmployee");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string username)
        { 
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            await client.DeleteAsync($"api/employee/delete/{username}");
            return RedirectToAction("ViewEmployee");
            
        }
        
        public IActionResult Login(UserLoginClass user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            var postTask = client.PostAsJsonAsync<UserLoginClass>("api/user/login", user);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("DashBoard");
            }
            return View();
        }
        
        public IActionResult Register(UserRegister user)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            var postTask = client.PostAsJsonAsync<UserRegister>("api/user/Register", user);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        public async Task<IActionResult> Update(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            TempModelClass? employee = new TempModelClass();

            HttpResponseMessage res = await client.GetAsync($"api/Employee/get/{username}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<TempModelClass>(result);
            }
            
            
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TempModelClass temp)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            var postTask = client.PostAsJsonAsync<TempModelClass>("api/Employee/Update", temp);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("viewEmployee");
            }
            return View();
        }
        public ActionResult designation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> designation(DesignationClass designationClass)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            var postTask = client.PostAsJsonAsync<DesignationClass>("api/Designation/designation", designationClass);

          /*  var postTask = client.PostAsJsonAsync<DesignationClass>("api/Designation/Designation", designationClass)*/
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("DashBoard");
            }
            return View();
        }
        
    }
}
