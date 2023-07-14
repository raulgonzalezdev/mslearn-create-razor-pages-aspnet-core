using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;
using System.Collections.Generic;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;

        public IList<Pizza> PizzaList { get; set; } = new List<Pizza>();
        [BindProperty]
        public Pizza NewPizza { get; set; }
        [BindProperty]
        public Pizza SelectedPizza { get; set; }

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
            SelectedPizza = new Pizza();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid || SelectedPizza == null)
            {
                return Page();
            }

            _service.UpdatePizza(SelectedPizza);

            return RedirectToAction("Get");
        }
    }
}
