using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Cmt.WebApi.Infrastructure.Attributes;
using Cmt.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICoursesService _courseService;

        public CourseController(
            IMapper mapper,
            ICoursesService coursesService)
        {
            _mapper = mapper;
            _courseService = coursesService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null) return NotFound();

            var model = Mapper.Map<CourseModel>(course);
            return Ok(model);
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> PostAsync([FromBody] CourseModel model)
        {
            var course = Mapper.Map<CourseDto>(model);
            var id = await _courseService.CreateAsync(course);

            return StatusCode(StatusCodes.Status201Created, Json(new { id }));
        }

        [HttpPut]
        [JwtAuthorize]
        public async Task<IActionResult> PutAsync([FromBody] CourseModel model)
        {
            var course = Mapper.Map<CourseDto>(model);
            await _courseService.UpdateAsync(course, new DateTime());

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        [JwtAuthorize]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _courseService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
