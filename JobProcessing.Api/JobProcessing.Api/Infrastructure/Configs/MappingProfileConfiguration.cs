using AutoMapper;
using JobProcessing.Data;
using JobProcessing.Api.DTO.Request;
using JobProcessing.Api.DTO.Response;

namespace JobProcessing.Api.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<CreateJobRequest, JobModel>();
            CreateMap<JobModel, JobResponse>();
        }
    }
}
