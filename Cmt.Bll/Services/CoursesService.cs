using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.DTOs.Courses;
using Cmt.Dal.Entities;
using Cmt.Dal.Repositories.Interfaces;

namespace Cmt.Bll.Services
{
    public class CoursesService: Service, ICoursesService
    {
        private readonly ICoursesRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICoursesRepository courseRepository)
            : base (unitOfWork)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public Task<int> CreateAsync(CourseDto course)
        {
            var entity = _mapper.Map<CourseEntity>(course);
            return _courseRepository.CreateAsync(entity);
        }

        public async Task<CourseDto> GetAsync(int id)
        {
            var entity = await _courseRepository.GetAsync(id);
            var dto = _mapper.Map<CourseDto>(entity);

            return dto;
        }
    }
}
