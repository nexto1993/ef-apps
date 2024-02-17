using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Excel.Pages.Select2
{
    public class IndexModel : PageModel
    {
        public List<SelectListItem> Options { get; set; }
        public int[] SelectedOptions { get; set; } // 

        public void OnGet()
        {
            Options = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Option 1" },
            new SelectListItem { Value = "2", Text = "Option 2" },
            // Add more options as needed
        };
        }
    }

}
