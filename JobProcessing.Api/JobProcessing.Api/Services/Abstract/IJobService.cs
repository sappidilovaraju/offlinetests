using JobProcessing.Api.DTO.Request;
using JobProcessing.Api.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobProcessing.Api.Services
{
    public interface IJobService
    {
        Task<JobResponse> CreateJob(CreateJobRequest job);
        JobResponse GetJobById(string id);
        IEnumerable<JobResponse> GetJobs(JobFilter filter);
    }
}
