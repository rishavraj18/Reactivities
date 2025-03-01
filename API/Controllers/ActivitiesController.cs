
using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{   
    // New method i.e. Primary constructor
    public class ActivitiesController(IMediator mediator) : BaseApiController
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
            return await mediator.Send(new GetActivityList.Query());
        }

        [HttpGet("{id}")] // api/activities/guid
        public async Task<ActionResult<Activity>> GetActivity(string id)
        {
            return await mediator.Send(new GetActivityDetails.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateActivity(Activity activity)
        {
            return await Mediator.Send(new CreateActivity.Command{Activity = activity});
        }

        [HttpPut]
        public async Task<ActionResult> EditActivity(Activity activity)
        {
            await Mediator.Send(new EditActivity.Command{Activity = activity});

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivity(string id)
        {
            await Mediator.Send(new DeleteActivity.Command{Id = id});

            return Ok();
        }
    }
}