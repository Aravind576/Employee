using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using UIUXLayer.Models;
namespace UIUXLayer.Controllers
{
    public class EmployeeController : Controller
    {
        
        public async Task<IActionResult> viewEmployee(string? ascend)
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");
                if (ascend == "ascend")
                {
                    List<ModelClass>? employee = new List<ModelClass>();

                    HttpResponseMessage res = await client.GetAsync("api/Employee/OrderByAscend");
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
                    }
                    return View(employee);
                }
                else if (ascend == "descend")
                {
                    List<ModelClass>? employee = new List<ModelClass>();

                    HttpResponseMessage res = await client.GetAsync("api/Employee/OrderBy");
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
                    }
                    return View(employee);
                }
                else
                {
                    List<ModelClass>? employee = new List<ModelClass>();

                    HttpResponseMessage res = await client.GetAsync("api/Employee");
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
                    }
                    return View(employee);
                }

            }
            return RedirectToAction("login");

        }
        public async Task<IActionResult> Details(string username)
        {
            if (HttpContext.Session.GetString("tokens") != null)
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
            return RedirectToAction("login");
            
            

        }
        public async Task<ActionResult> create()
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");
                List<DesignationClass>? designationTemp = new List<DesignationClass>();

                HttpResponseMessage res = await client.GetAsync("api/Designation");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                    ViewData["designationtemp"] = designationTemp;
                }
                return View();
            }
            return RedirectToAction("login");
            
        }
        [HttpPost]
        public async Task<IActionResult> create(ModelClass emp)
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {

                
                byte[] bytes = null;
                using (Stream fs = emp.imageUpload.OpenReadStream())
                {
                    using(BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }
                emp.imagePath = Convert.ToBase64String(bytes, 0, bytes.Length);
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");
                List<DesignationClass>? designationTemp = new List<DesignationClass>();

                HttpResponseMessage res = await client.GetAsync("api/Designation");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                    ViewData["designationtemp"] = designationTemp;
                }
                var postTask = client.PostAsJsonAsync<ModelClass>("api/Employee/create", emp);

                postTask.Wait();
                var Result = postTask.Result;
                if (Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewEmployee");
                }
                return View();
            }
            return RedirectToAction("login");
            
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
                HttpContext.Session.SetString("tokens", "token");
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
            if (HttpContext.Session.GetString("tokens") != null)
            {
                return View();
            }
            return RedirectToAction("login");
        }
        public async Task<IActionResult> Update(string username)
        {
            
            if (HttpContext.Session.GetString("tokens") != null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");
                List<DesignationClass>? designationTemp = new List<DesignationClass>();

                HttpResponseMessage des = await client.GetAsync("api/Designation");

                if (des.IsSuccessStatusCode)
                {
                    var result = des.Content.ReadAsStringAsync().Result;
                    designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                    ViewData["designationtemp"] = designationTemp;
                }

                ModelClass employee = new ModelClass();
                HttpResponseMessage res = await client.GetAsync($"api/Employee/get/{username}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    employee = JsonConvert.DeserializeObject<ModelClass>(result);
                }


                return View(employee);
            }
            return RedirectToAction("login");
        }
        [HttpPost]
        public async Task<IActionResult> Update(ModelClass temp)
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");

                List<DesignationClass>? designationTemp = new List<DesignationClass>();
                HttpResponseMessage des = await client.GetAsync("api/Designation");

                if (des.IsSuccessStatusCode)
                {
                    var result = des.Content.ReadAsStringAsync().Result;
                    designationTemp = JsonConvert.DeserializeObject<List<DesignationClass>>(result);
                    ViewData["designationtemp"] = designationTemp;
                }

                var postTask = client.PostAsJsonAsync<ModelClass>("api/Employee/Update", temp);
                postTask.Wait();
                var Result = postTask.Result;
                if (Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("viewEmployee");
                }
                return View();
            }
            return RedirectToAction("login");

        }
        public ActionResult designation()
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {
                return View();
            }
            return RedirectToAction("login");
        }
        [HttpPost]
        public async Task<IActionResult> designation(DesignationClass designationClass)
        {
            if (HttpContext.Session.GetString("tokens") != null)
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
            return RedirectToAction("login");
            
        }
        public async Task<IActionResult> ViewDesignation()
        {
            if (HttpContext.Session.GetString("tokens") != null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7015");
                List<ModelClass>? employee = new List<ModelClass>();

                HttpResponseMessage res = await client.GetAsync("api/designation");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    employee = JsonConvert.DeserializeObject<List<ModelClass>>(result);
                }
                return View(employee);
            }
            return RedirectToAction("login");
            

        }
        public ActionResult logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Clear();
               
            
            return RedirectToAction("login");
        }

    }
}
