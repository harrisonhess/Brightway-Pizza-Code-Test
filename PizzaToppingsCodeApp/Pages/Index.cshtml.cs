using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PizzaToppingsCodeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var client = new WebClient();
            var json = client.DownloadString("https://www.brightway.com/CodeTests/pizzas.json");
            var pizzas = JsonConvert.DeserializeObject<Pizza[]>(json);

            int count = pizzas.Length;
            List<string> allToppings = new List<string>();
                       
            foreach (Pizza p in pizzas)
            {
                foreach (string s in p.toppings)
                {
                    allToppings.Add(s);
                }
            }

            var toppingData = new List<string>(allToppings.OrderByDescending(x => x.Count()));
            var pieData = new List<string>(pizzas.);

            ViewData["uniques"] = toppingData;
            ViewData["uniquePies"] = pieData;
        }
    }
}
