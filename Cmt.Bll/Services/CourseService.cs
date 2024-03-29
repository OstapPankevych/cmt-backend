﻿using System.Threading.Tasks;
using AutoMapper;
using Cmt.Bll.Services.Interfaces;
using Cmt.Bll.DTOs.Courses;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces;
using Cmt.Dal.Interfaces.Repositories;
using Cmt.Bll.Services.Exceptions;
using System.Collections.Generic;
using System;

namespace Cmt.Bll.Services
{
    public class CoursesService: Service, ICoursesService
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesService(
            IUnitOfWork unitOfWork,
            ICourseRepository courseRepository)
            : base (unitOfWork)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IList<CourseDto>> GetAsync()
        {
            var entities = await _courseRepository.GetAsync();
            var dtos = Mapper.Map<IList<CourseDto>>(entities);

            return dtos;
        }

        public async Task<CourseDto> GetAsync(int id)
        {
            var entity = await _courseRepository.GetAsync(id);
            var dto = Mapper.Map<CourseDto>(entity);

            return dto;
        }

        public async Task<CourseDto> CreateAsync(CourseDto dto)
        {
            var entity = Mapper.Map<CourseEntity>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _courseRepository.AddAsync(entity);
            var course = Mapper.Map<CourseDto>(entity);

            return course;
        }

        public async Task UpdateAsync(CourseDto dto)
        {
            var dbEntity = await GetRequiredEntity(_courseRepository, dto.Id);

            UpdateEntity(dto, dbEntity);

            await _courseRepository.UpdateAsync(dbEntity);
        }

        public async Task DeleteAsync(CourseDto dto)
        {
            var dbEntity = await GetRequiredEntity(_courseRepository, dto.Id);

            await _courseRepository.RemoveAsync(dbEntity);
        }
    }
}
