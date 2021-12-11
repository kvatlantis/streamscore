using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using StreamScore.Hubs;

namespace StreamScore.Pages
{
  public class ThuisModel : PageModel
  {
    private readonly IHubContext<ScoreHub> _scoreHub;

    public ThuisModel(IHubContext<ScoreHub> scoreHub)
    {
      _scoreHub = scoreHub;
    }
    public async Task<IActionResult> OnGet()
    {
      Globals.Thuis++;
      
      await _scoreHub.Clients.All.SendAsync("ReceiveScore", $"{Globals.Thuis} - {Globals.Uit}");
      
      return Redirect("/");
    }
  }
}
