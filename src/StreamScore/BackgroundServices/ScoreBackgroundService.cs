using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamScore.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StreamScore.BackgroundServices
{
  public class ScoreBackgroundService : IHostedService, IDisposable
  {
    private int executionCount = 0;
    private readonly ILogger<ScoreBackgroundService> _logger;
    private readonly IHubContext<ScoreHub> _scoreHub;
    private Timer _timer = null!;

    public ScoreBackgroundService(ILogger<ScoreBackgroundService> logger, IHubContext<ScoreHub> scoreHub)
    {
      _logger = logger;
      _scoreHub = scoreHub;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service running.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

      return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
      //var count = Interlocked.Increment(ref executionCount);

      //_logger.LogInformation(
      //    "Timed Hosted Service is working. Count: {Count}", count);

      if (Globals.State == GameState.Running)
        Globals.Time = Globals.Time - 1;


      //_scoreHub.Clients.All.SendAsync("ReceiveScore", $"{Globals.Thuis} - {Globals.Uit}");

      var info = Globals.BuildInfo();
      _scoreHub.Clients.All.SendAsync("ScreenUpdate", info);

    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
