using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Cmt.WebApi.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Models.Courses;
using Cmt.WebApi.Models;
using System.Collections.Generic;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : CmtController
    {
        private readonly IMapper _mapper;
        private readonly ICoursesService _courseService;
        private readonly IAuthorizationService _authorizationService;

        public CoursesController(
            IMapper mapper,
            ICoursesService coursesService,
            IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _courseService = coursesService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var courses = await _courseService.GetAsync();

            var models = Mapper.Map<IList<Course>>(courses);
            return Ok(CreateResponse(models));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<Course>(course);
            return Ok(CreateResponse(model));
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> PostAsync([FromBody] Course model)
        {
            var course = Mapper.Map<CourseDto>(model);
            course.UpdatedBy = GetCurrentUserId();
            var id = await _courseService.CreateAsync(course);

            return Created(CreateResponse(new Course { Id = id }));
        }

        [HttpPut]
        [JwtAuthorize]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Course model)
        {
            var dbCource = await _courseService.GetAsync(id);
            if (dbCource == null)
            {
                return NotFound();
            }

            var isOwner = await _authorizationService.AuthorizeAsync(
                User, dbCource, Policies.CourseOwner);

            if (!isOwner.Succeeded)
            {
                return Forbid();
            }

            var course = Mapper.Map<CourseDto>(model);

            course.Id = id;
            course.UpdatedBy = GetCurrentUserId();
            course.UpdatedAt = GetLastModifiedUtcHeader();

            await _courseService.UpdateAsync(course);

            return NoContent();
        }

        [HttpDelete]
        [JwtAuthorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _courseService.DeleteAsync(id);

            return NoContent();
        }
    }
}
