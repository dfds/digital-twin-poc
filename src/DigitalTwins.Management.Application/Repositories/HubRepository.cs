using CloudEngineering.CodeOps.Infrastructure.EntityFramework.Repositories;
using DigitalTwins.Management.Application.Data;
using DigitalTwins.Management.Domain.Aggregates;
using DigitalTwins.Management.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalTwins.Management.Application.Repositories
{
    public class HubRepository : EntityFrameworkRepository<HubRoot, ApplicationContext>, IHubRepository
    {
        public HubRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<HubRoot>> GetAsync(Expression<Func<HubRoot, bool>> filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _context.Hubs.AsQueryable()
                                            .AsNoTracking()
                                            .Where(filter)
                                            .Include(i => i.Devices)
                                            .AsEnumerable();
            });
        }

        public async Task<HubRoot> GetAsync(Guid hubId)
        {
            var hubRoot = await _context.Hubs.FindAsync(hubId);

            if (hubRoot != null)
            {
                var entry = _context.Entry(hubRoot);

                if (entry != null)
                {
                    await entry.Reference(i => i.Devices).LoadAsync();
                }
            }

            return hubRoot;
        }
    }
}