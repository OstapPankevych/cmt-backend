using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Models.Courses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : CmtController
    {
        private readonly ICoursesService _courseService;

        public CoursesController(
            ICoursesService coursesService,
            IAuthorizationService authorizationService)
            : base(authorizationService)
        {
            _courseService = coursesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var courses = await _courseService.GetAsync();

            var models = Mapper.Map<IList<Course>>(courses);
            var result = new {courses = models};

            return Ok(result);
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
            var result = new {course = model};

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.CourseCreator)]
        public async Task<IActionResult> PostAsync(
            [FromBody][Required] Course model)
        {
            var course = Mapper.Map<CourseDto>(model);
            var userOwnerId = GetRequireCurrentUserId();
            course.CreatedBy = userOwnerId;

            var newCourse = await _courseService.CreateAsync(course);
            var newModel = Mapper.Map<Course>(newCourse);

            return Created(newModel);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync(int id,
            [FromBody][Required] Course model)
        {
            var courseDto = await _courseService.GetAsync(id);
            if (courseDto == null)
            {
                return NotFound();
            }

            var isCourseOwner = await IsAuthorizeed(Policies.CourseOwner, courseDto);
            if (!isCourseOwner)
            {
                return Forbid();
            }

            var course = Mapper.Map<CourseDto>(model);

            course.Id = id;
            course.UpdatedAt = GetRequireLastModifierUtcHeader();

            await _courseService.UpdateAsync(course);

            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var isCourseOwner = await IsAuthorizeed(Policies.CourseOwner, course);
            if (!isCourseOwner)
            {
                return Forbid();
            }

            await _courseService.DeleteAsync(course);

            return NoContent();
        }
    }
}
