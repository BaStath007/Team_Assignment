﻿using AutoMapper;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Profiles
{
    public class Profile: AutoMapper.Profile
    {        
        public Profile()
        {  
            CreateMap<Topic,TopicDto>();

            CreateMap<Topic, TopicDto>()

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfPossibleMarks, opt => opt.MapFrom(src => src.NumberOfPossibleMarks))
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
                .ForMember(dest => dest.Stems, opt => opt.MapFrom(src => src.Stems))
                .ReverseMap();
               


            CreateMap<Stem, StemDto>();

            CreateMap<Stem, StemDto>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForMember(dest => dest.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForMember(dest => dest.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForMember(dest => dest.OptionD, opt => opt.MapFrom(src => src.OptionD))
                .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForMember(dest => dest.TopicDescription, opt => opt.MapFrom(src => src.Topic.Description))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
                .ReverseMap();


            CreateMap<Exam, ExamDto>();

            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.TitleOfCertificate, opt => opt.MapFrom(src => src.Certificate.TitleOfCertificate))
                .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
                .ForMember(dest => dest.ExamStems, opt => opt.MapFrom(src => src.ExamStems))
                .ForMember(dest => dest.Stems, opt => opt.MapFrom(src => src.ExamStems.Select(exs => exs.Stem)))
                .ReverseMap();

            CreateMap<Stem, ExamStem>();            

            CreateMap<Stem, ExamStem>()
                .ForPath(dest => dest.Stem.Question, opt => opt.MapFrom(src => src.Question))
                .ForPath(dest => dest.Stem.OptionA, opt => opt.MapFrom(src => src.OptionA))
                .ForPath(dest => dest.Stem.OptionB, opt => opt.MapFrom(src => src.OptionB))
                .ForPath(dest => dest.Stem.OptionC, opt => opt.MapFrom(src => src.OptionC))
                .ForPath(dest => dest.Stem.OptionD, opt => opt.MapFrom(src => src.OptionD))
                .ForPath(dest => dest.Stem.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
                .ForPath(dest => dest.Stem.Topic, opt => opt.MapFrom(src => src.Topic))
                .ReverseMap();
            
            CreateMap<ExamStem, CandidateExamStem>();

            CreateMap<ExamStem, CandidateExamStem>()
                .ForPath(dest => dest.ExamStem.Stem.Question, opt => opt.MapFrom(src => src.Stem.Question))
                .ForPath(dest => dest.ExamStem.Stem.OptionA, opt => opt.MapFrom(src => src.Stem.OptionA))
                .ForPath(dest => dest.ExamStem.Stem.OptionB, opt => opt.MapFrom(src => src.Stem.OptionB))
                .ForPath(dest => dest.ExamStem.Stem.OptionC, opt => opt.MapFrom(src => src.Stem.OptionC))
                .ForPath(dest => dest.ExamStem.Stem.OptionD, opt => opt.MapFrom(src => src.Stem.OptionD))
                .ForPath(dest => dest.ExamStem.Stem.CorrectAnswer, opt => opt.MapFrom(src => src.Stem.CorrectAnswer))
                .ReverseMap();
        }
    }
}
