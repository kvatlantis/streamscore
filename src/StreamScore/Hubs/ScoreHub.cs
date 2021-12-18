using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace StreamScore.Hubs
{
  public class ScoreHub : Hub
  {
    public override async Task OnConnectedAsync()
    {
      await base.OnConnectedAsync();

      var info = Globals.BuildInfo();
      await Clients.Caller.SendAsync("ScreenUpdate", info);
    }

    private async Task Update()
    {
      var info = Globals.BuildInfo();
      await Clients.All.SendAsync("ScreenUpdate", info);
    }

    public async Task SendMessage(string user, string message)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message);
      await UpdateScore();
    }

    public async Task UpdateScore()
    {
      await Clients.All.SendAsync("ReceiveScore", $"{Globals.Thuis} - {Globals.Uit}");
    }

    public async Task Team1Plus()
    {
      Globals.Thuis++;
      await Update();
    }

    public async Task Team1Min()
    {
      Globals.Thuis--;
      if (Globals.Thuis < 0)
        Globals.Thuis = 0;
      await Update();
    }

    public async Task Team2Plus()
    {
      Globals.Uit++;
      await Update();
    }

    public async Task Team2Min()
    {
      Globals.Uit--;
      if (Globals.Uit < 0)
        Globals.Uit = 0;
      await Update();
    }

    public async Task StateRun()
    {
      Globals.State = GameState.Running;
      await Update();
    }

    public async Task StateStop()
    {
      Globals.State = GameState.Pause;
      await Update();
    }

    public async Task Reset()
    {
      Globals.State = GameState.Pause;
      Globals.Thuis = 0;
      Globals.Uit = 0;
      await Update();
    }

    public async Task SetData(GameInfo info)
    {
      Globals.Team1 = info.Team1;
      Globals.Team1Name = info.Team1Name;
      Globals.Team2 = info.Team2;
      Globals.Team2Name = info.Team2Name;
      Globals.Status = info.Status;
      Globals.Helft = info.Helft;
      await Update();
    }

    public async Task Klok(string value)
    {
      var min = Convert.ToInt32(value);
      Globals.Time = min * 60;
      await Update();
    }


  }
}