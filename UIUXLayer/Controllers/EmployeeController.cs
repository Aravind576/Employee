using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            //
        }
    }
}
