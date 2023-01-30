﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Services
{
    public class ExamStemService : ControllerBase, IExamStemService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public ExamStemService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.ExamStems == null || await _unit.ExamStem.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
            }
            else
            {
                _myDTO.View = "Details";
                ExamStem examStem = await _unit.ExamStem.GetAsync(id);
                _myDTO.ExamStemDto = _mapper.Map<ExamStemDto>(examStem);
            }
            return _myDTO;
        }
        public async Task<IEnumerable<ExamStemDto>?> GetAll()
        {
            var examStems = await _unit.ExamStem.GetAllAsync();
            _myDTO.ExamStemDtos = _mapper.Map<List<ExamStemDto>>(examStems);
            return _myDTO.ExamStemDtos;
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.ExamStems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                return _myDTO;
            }
            ExamStem examStem = await _unit.ExamStem.GetAsync(id);
            _myDTO.ExamStemDto = _mapper.Map<ExamStemDto>(examStem);
            if (_myDTO.ExamStemDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddOrUpdate(int id, ExamStemDto examStemDto)
        {
            //Certificate certificate = await _unit.Certificate.GetAsyncByTilteOfCert(examStemDto.TitleOfCertificate);
            //examStemDto.Certificate = certificate;
            ExamStem examStem = _mapper.Map<ExamStem>(examStemDto);

            EntityState state = _unit.ExamStem.AddOrUpdate(examStem);
            if (id != examStem.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The exam stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.ExamStemDto = examStemDto;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested exam stem has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested exam stem has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.ExamStem.Exists(examStem.Id))
                {
                    _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                }
                //if (await _unit.ExamStem.DescriptionExists(examStem.Id, examStem.Description))
                //{
                //    if (state == EntityState.Added)
                //    {
                //        _myDTO.View = "Create";
                //    }
                //    if (state == EntityState.Modified)
                //    {
                //        _myDTO.View = "Edit";
                //    }
                //    _myDTO.Message = "This exam stem already exists. Please try again later.";
                //    _myDTO.ExamStemDto = examStemDto;                    
                //    return _myDTO;
                //}
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                IEnumerable<ExamStem> examStems = await _unit.ExamStem.GetAllAsync();
                _myDTO.ExamStemDtos = _mapper.Map<List<ExamStemDto>>(examStems);
                return _myDTO;
            }
            else
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.ExamStemDto = examStemDto;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.ExamStems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                IEnumerable<ExamStem> examStems = await _unit.ExamStem.GetAllAsync();
                _myDTO.ExamStemDtos = _mapper.Map<List<ExamStemDto>>(examStems);
                return _myDTO;
            }
            ExamStem examStem = await _unit.ExamStem.GetAsync(id);
            _myDTO.ExamStemDto = _mapper.Map<ExamStemDto>(examStem);
            if (_myDTO.ExamStemDto == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                IEnumerable<ExamStem> examStems = await _unit.ExamStem.GetAllAsync();
                _myDTO.ExamStemDtos = _mapper.Map<List<ExamStemDto>>(examStems);
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested exam stem has been deleted successfully.";
            if (!await _unit.ExamStem.Exists(id))
            {
                _myDTO.Message = "The requested exam stem could not be found. Please try again later.";
                return _myDTO;
            }
            ExamStem examStem = await _unit.ExamStem.GetAsync(id);
            _unit.ExamStem.Delete(examStem);
            await _unit.SaveAsync();
            IEnumerable<ExamStem> examStems = await _unit.ExamStem.GetAllAsync();
            _myDTO.ExamStemDtos = _mapper.Map<List<ExamStemDto>>(examStems);
            return _myDTO;
        }
    }
}