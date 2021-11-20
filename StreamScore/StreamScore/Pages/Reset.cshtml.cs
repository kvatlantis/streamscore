using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StreamScore.Pages
{
  public class ResetModel : PageModel
  {
    public IActionResult OnGet()
    {

      Globals.Thuis = 0;
      Globals.Uit = 0;

      return Redirect("/");
    }
  }
}
