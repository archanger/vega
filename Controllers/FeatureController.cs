using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Persistance;

namespace vega.Controllers
{
    public class FeatureController : Controller
    {
        public VegaDbContext Context { get; }
        public FeatureController(VegaDbContext context)
        {
            this.Context = context;
        }

        [HttpGet("/api/features")]
        public IEnumerable<Feature> GetFeatures()
        {
            return Context.Features;
        }
    }
}