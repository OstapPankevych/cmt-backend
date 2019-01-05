using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Bll.Services
{
    public class CoursesService: Service, ICoursesService
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICourseRepository courseRepository)
            : base (mapper, unitOfWork)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseDto> GetAsync(int id)
        {
            var entity = await _courseRepository.GetAsync(id);
            var dto = Mapper.Map<CourseDto>(entity);

            return dto;
        }

        public async Task<int> CreateAsync(CourseDto dto)
        {
            Create(dto);
            var entity = Mapper.Map<CourseEntity>(dto);
            await _courseRepository.AddAsync(entity);

            return entity.Id;
        }

        public async Task UpdateAsync(CourseDto dto)
        {
            var dbEntity = await _courseRepository.GetAsync(dto.Id);
            Update(dto, dbEntity, dto.UpdatedAt);

            if (dto.Name != null) dbEntity.Name = dto.Name;

            await _courseRepository.AddAsync(dbEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _courseRepository.GetAsync(id);
        }
    }
}
