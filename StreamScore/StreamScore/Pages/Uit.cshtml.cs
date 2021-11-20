using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StreamScore.Pages
{
  public class UitModel : PageModel
  {
    public IActionResult OnGet()
    {
      Globals.Uit++;

      return Redirect("/");

    }
  }
}
