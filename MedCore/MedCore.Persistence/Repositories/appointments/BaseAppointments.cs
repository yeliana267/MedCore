using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.DependencyInjection;

public class BaseAppointments : IBaseAppointments
{
    private readonly MedCoreContext _context;
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed = false;

    public IAppointmentsRepository AppointmentsRepository=> _serviceProvider.GetRequiredService<IAppointmentsRepository>();
    public IDoctorAvailabilityRepository DoctorAvailabilityRepository => _serviceProvider.GetRequiredService<IDoctorAvailabilityRepository>();
    public BaseAppointments(MedCoreContext context,IServiceProvider serviceProvider
)
    {
        _context = context;
        _serviceProvider = serviceProvider;

    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            _disposed = true;
        }
    }
}
