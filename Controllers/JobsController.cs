using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobService _jobService;

        public JobsController(JobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public ActionResult<List<Job>> Get() =>
            _jobService.Get();

        [HttpGet("{id:length(24)}", Name = "GetJob")]
        public ActionResult<Job> Get(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        [HttpPost]
        public ActionResult<Job> Create(Job job)
        {
            _jobService.Create(job);

            return CreatedAtRoute("GetJob", new { id = job.Id.ToString() }, job);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Job jobIn)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            _jobService.Update(id, jobIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var job = _jobService.Get(id);

            if (job == null)
            {
                return NotFound();
            }

            _jobService.Remove(job.Id);

            return NoContent();
        }
    }
}
