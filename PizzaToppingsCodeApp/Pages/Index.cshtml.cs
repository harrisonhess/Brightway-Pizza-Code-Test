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
            var pieData = new List<Pizza>();

            //int count = pizzas.Length;
            List<string> allToppings = new List<string>();
                       
            foreach (Pizza p in pizzas)
            {
                foreach (string s in p.toppings)
                {
                    allToppings.Add(s);
                }
                pieData = pizzas.GroupBy(x => x)
                                     .Where(y => y.Count() > 1)
                                     .Select(z => z.Key)
                                     .ToList();
            }

            //get count of each unique topping and order by count
            //find duplicate pizzas and order by count

            var toppingData = new List<string>(allToppings);
            toppingData = toppingData.GroupBy(x => x)
                                     .Where(y => y.Count() > 1)
                                     .Select(z => z.Key)
                                     .ToList();
            //var pieData = new List<string>(!pizzas.Distinct());
            Console.WriteLine(pizzas[0].toppings.SequenceEqual(pizzas[1].toppings));

            ViewData["uniques"] = toppingData;
            ViewData["uniquePies"] = pieData;
        }
    }
}