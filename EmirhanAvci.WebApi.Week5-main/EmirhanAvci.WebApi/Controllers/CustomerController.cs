using EmirhanAvci.WebApi.DataParticles.Abstract;
using EmirhanAvci.WebApi.Helpers.Paging;
using EmirhanAvci.WebApi.Helpers.Paging.Model;
using EmirhanAvci.WebApi.Models.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public CustomerController(IDistributedCache distributedCache, IMemoryCache memoryCache, ICustomerService customerService)
        {
            _customerService = customerService;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }
        [HttpGet("all")]
        [ResponseCache(Duration = 10000, Location = ResponseCacheLocation.Any)]
        public IActionResult GetAll()
        {
            return Ok(_customerService.GetAll());
        }

        // Todo: Filtering / Sorting / Searching
        [HttpGet]

        public IActionResult Get([FromQuery] PagingQueryParams pagingParams)        ///customer?page=2&pageSize=12
        {
            //get memory cache
            if (_memoryCache.TryGetValue("Customers", out List<Customer> customers))
            {
                return Ok(customers);
            }

            var list = _customerService.GetCustomers(pagingParams);
            Response.Headers.Add("X-Paging", System.Text.Json.JsonSerializer.Serialize(list.Result));

            //Add memory cache
            _memoryCache.Set("Customers", list, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
            });
            return Ok(list);
        }

        [HttpGet("ResponseCache")]
        [ResponseCache(Duration = 10000, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "page,pageSize" })]
        public IActionResult GetResponseCache([FromQuery] PagingQueryParams pagingParams)        ///customer?page=2&pageSize=12
        {
            var list = _customerService.GetCustomers(pagingParams);
            Response.Headers.Add("X-Paging", System.Text.Json.JsonSerializer.Serialize(list.Result));

            return Ok(list);
        }

        [HttpGet("DistrubutedCache")]
        public async Task<IActionResult> GetDistrubutedCache([FromQuery] PagingQueryParams pagingParams)        ///customer?page=2&pageSize=12
        {
            if (!string.IsNullOrWhiteSpace(await _distributedCache.GetStringAsync("CustomerDistCache")))
            {
               return Ok(await _distributedCache.GetStringAsync("CustomerDistCache"));
            }
                var list = _customerService.GetCustomers(pagingParams);
                Response.Headers.Add("X-Paging", System.Text.Json.JsonSerializer.Serialize(list.Result));

                if (list.Count > 100)
                {
                    await _distributedCache.SetStringAsync("CustomersDistCache", (System.Text.Json.JsonSerializer.Serialize(list)));
                }

                return Ok(list);
            
        }

    }

}
