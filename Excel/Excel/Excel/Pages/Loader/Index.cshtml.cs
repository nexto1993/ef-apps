using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Excel.Pages.Loader
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string EntriesUrl { get; set; }
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            var entriesUrl = _configuration.GetValue<string>("ApiSettings:EntriesUrl");
            // Pass the URL to the view
            EntriesUrl = entriesUrl;
        }
    }
}
