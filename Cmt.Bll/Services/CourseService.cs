using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Common.DTOs.Courses;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Bll.Services
{
    public class CoursesService: Service, ICoursesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;

        public CoursesService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICourseRepository courseRepository)
            : base (unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<int> CreateAsync(CourseDto course)
        {
            var entity = _mapper.Map<CourseEntity>(course);
            await _courseRepository.AddAsync(entity);

            return entity.Id;
        }

        public async Task<CourseDto> GetAsync(int id)
        {
            var entity = await _courseRepository.GetAsync(id);
            var dto = _mapper.Map<CourseDto>(entity);

            return dto;
        }
    }
}
