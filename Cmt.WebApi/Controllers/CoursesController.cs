using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Cmt.WebApi.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cmt.WebApi.Infrastructure.Constants;
using Cmt.WebApi.Models.Courses;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : CmtController
    {
        private readonly IMapper _mapper;
        private readonly ICoursesService _courseService;
        private readonly IAuthorizationService _authorizationService;

        public CourseController(
            IMapper mapper,
            ICoursesService coursesService,
            IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _courseService = coursesService;
            _authorizationService = authorizationService;
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
            return Ok(new CourseResponse { Course = model });
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> PostAsync([FromBody] Course model)
        {
            var course = Mapper.Map<CourseDto>(model);
            var id = await _courseService.CreateAsync(course);

            return Created(CreateResponse(new Course { Id = id }));
        }

        [HttpPut]
        [JwtAuthorize(Policy = Policies.CourseOwner)]
        public async Task<IActionResult> PutAsync([FromBody] Course model)
        {
            var cource = await _courseService.GetAsync(model.Id);
            var isOwner = await _authorizationService.AuthorizeAsync(
                User, cource, Policies.CourseOwner);

            if (!isOwner.Succeeded)
            {
                return Forbid();
            }

            var course = Mapper.Map<CourseDto>(model);

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

        private CourseResponse CreateResponse(Course course)
            => new CourseResponse { Course = course };
    }
}
