using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public class AdminSetupHostedService : IHostedService
{
    private readonly AdminSetupService _adminSetupService;

    public AdminSetupHostedService(AdminSetupService adminSetupService)
    {
        _adminSetupService = adminSetupService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _adminSetupService.SetupAdminUser();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Nothing to do here.
        return Task.CompletedTask;
    }
}

}