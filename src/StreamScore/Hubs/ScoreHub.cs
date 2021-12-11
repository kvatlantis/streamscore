using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace StreamScore.Hubs
{
  public class ScoreHub : Hub
  {
    public async Task SendMessage(string user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
      await UpdateScore();
    }

    public async Task UpdateScore()
    {
      await Clients.All.SendAsync("ReceiveScore", $"{Globals.Thuis} - {Globals.Uit}");
    }
  }
}