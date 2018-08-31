using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.DTOs;
using Cmt.Common.Identity;
using Cmt.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICoursesService coursesService, IMapper mapper)
        {
            _courseService = coursesService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CourseModel>(course);
            return Ok(model);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PostAsync([FromBody] CourseModel model)
        {
            var course = _mapper.Map<CourseDto>(model);
            var id = await _courseService.CreateAsync(course);

            return Ok(id);
        }
    }
}
