using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CourseNetworkAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class GetMultipleCoursesController : Controller
    {
        private const int CACHE_TIMEOUT = 2;
        private IMemoryCache _cache;
        public GetMultipleCoursesController(IMemoryCache memcache) {
            _cache = memcache;
        }

        [HttpGet]
        public string Get(string input) {
            List<int> courses = JsonConvert.DeserializeObject<List<int>>(input);
            CourseNetwork cn;
            if (!_cache.TryGetValue(CacheKeys.CourseNetwork, out cn))
            {
                // Key Not in cache, so lets create new data.
                cn = new CourseNetwork();
                cn.BuildNetwork();

                // Set cache Options
                var cOptions = new MemoryCacheEntryOptions()
                    // Set the expiration
                    .SetSlidingExpiration(TimeSpan.FromSeconds(CACHE_TIMEOUT));

                // Save Data in cache
                _cache.Set(CacheKeys.CourseNetwork, cn, cOptions);
            }
            Dictionary<int, List<CourseNode>> results = new Dictionary<int, List<CourseNode>>();
            
            foreach (int course in courses) {
                List<CourseNode> temp;
                if (!results.TryGetValue(course, out temp)) 
                    results.Add(course, cn.FindShortPath(course));
            }
            return JsonConvert.SerializeObject(results, Formatting.Indented);
        }
    }
}