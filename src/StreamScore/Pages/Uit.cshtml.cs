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
  public class UitModel : PageModel
  {
    private readonly IHubContext<ScoreHub> _scoreHub;

    public UitModel(IHubContext<ScoreHub> scoreHub)
    {
      _scoreHub = scoreHub;
    }
    public async Task<IActionResult> OnGet()
    {
      Globals.Uit++;

      await _scoreHub.Clients.All.SendAsync("ReceiveScore", $"{Globals.Thuis} - {Globals.Uit}");

      return Redirect("/");
    }
  }

}
