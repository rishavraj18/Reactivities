
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{   
    // New method i.e. Primary constructor
    public class ActivitiesController(AppDbContext context) : BaseApiController
    {   
        // Old method
        
        // private readonly AppDbContext _context;
        // public ActivitiesController(AppDbContext context) 
        // {
        //    _context = context; // same as this.context = context;
            
        // }

        [HttpGet] // api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] // api/activities/guid
        public async Task<ActionResult<Activity>> GetActivity(string id)
        {
            var activity = await context.Activities.FindAsync(id);

            if(activity == null) return NotFound(); // 404

            return activity;
        }
    }
}