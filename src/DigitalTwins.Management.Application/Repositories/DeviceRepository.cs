using CloudEngineering.CodeOps.Infrastructure.EntityFramework.Repositories;
using DigitalTwins.Management.Application.Data;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Repositories
{
    public class DeviceRepository : EntityFrameworkRepository<DeviceRoot, ApplicationContext>, IDeviceRepository
    {
        public DeviceRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<DeviceRoot>> GetAsync(Expression<Func<DeviceRoot, bool>> filter, CancellationToken cancellationToken = default)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _context.Devices.AsQueryable()
                                            .AsNoTracking()
                                            .Where(filter)
                                            .AsEnumerable();
            }, cancellationToken);
        }

        public async Task<DeviceRoot> GetAsync(Guid deviceId, CancellationToken cancellationToken = default)
        {
            return await _context.Devices.FindAsync(new object[] { deviceId }, cancellationToken: cancellationToken);
        }
    }
}