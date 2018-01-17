using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Core.Models;
using vega.Extrnsions;

namespace vega.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryobject) 
        {
            var result = new QueryResult<Vehicle>();

            var query = context.Vehicles
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .AsQueryable();
            
            if (queryobject.MakeId.HasValue) {
                query = query.Where(v => v.Model.MakeId == queryobject.MakeId);
            }

            if (queryobject.ModelId.HasValue) {
                query = query.Where(v => v.ModelId == queryobject.ModelId);
            }

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contctName"] = v => v.ContactName,
                ["id"] = v => v.Id,
            };

            query = query.ApplyOrdering(queryobject, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPageing(queryobject);

            result.items = await query.ToListAsync();

            return result;
        }

        
    }
}