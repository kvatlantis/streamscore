using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamScore
{
  public static class Globals
  {

    public static int Thuis { get; set; }
    public static int Uit { get; set; }

    public static string Team1 { get; set; } = "ATL";
    public static string Team2 { get; set; } = "ATL";
    public static string Team1Name { get; set; } = "Atlantis 1";
    public static string Team2Name { get; set; } = "Atlantis 1";
    public static string Status { get; set; }
    public static string Helft { get; set; } = "1e helft";


    public static GameState State { get; set; } = GameState.Pause;
    public static int Time { get; set; } = 25 * 60;


    public static GameInfo BuildInfo()
    {
      var result = new GameInfo()
      {
        Team1 = Team1,
        Team2 = Team2,
        Team1Name = Team1Name,
        Team2Name = Team2Name,
        Score1 = Thuis,
        Score2 = Uit,
        Time = "",
        Helft = Helft,
        Status = Status,
      };

      if (State != GameState.None)
      {
        if (Time > 60)
        {
          var ts = TimeSpan.FromSeconds(Time);
          result.Time = $"{ts.Minutes}:{ts.Seconds:00}";
        }
        else
        {
          result.Time = "1:00";
        }
      }
      return result;
    }
  }

  public enum GameState
  {
    None,
    WaitForStart,
    Running,
    Pause,
  }

  public class GameInfo
  {
    public string Team1 { get; set; }
    public string Team2 { get; set; }
    public string Team1Name { get; set; }
    public string Team2Name { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public string Time { get; set; }
    public string Helft { get; set; }
    public string Status { get; set; }

  }
}
