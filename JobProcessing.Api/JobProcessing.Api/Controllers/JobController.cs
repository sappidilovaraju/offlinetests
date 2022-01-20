using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using JobProcessing.Api.DTO.Request;
using JobProcessing.Api.DTO.Response;
using JobProcessing.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace JobProcessing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        #region Private Variables

        private readonly IJobService _jobService;
        private readonly ILogger<JobController> _logger;
        
        #endregion

        #region Constructor

        public JobController(IJobService jobService,
            ILogger<JobController> logger)
        {
            _jobService = jobService
                ?? throw new ArgumentNullException(nameof(jobService));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region CRUD - C

        /// <summary>
        /// Creates new Job
        /// </summary>
        /// <returns>Returns Job</returns>
        /// <response code="200">Returns Job</response>
        [HttpPost]
        [ProducesResponseType(typeof(JobResponse), Status200OK)]
        public async Task<ApiResponse> CreateJob([FromBody] CreateJobRequest dto)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());

            try
            {
                var result = await _jobService.CreateJob(dto);
                return new ApiResponse("Job Created Succesfully", result, Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateJob - Exception: {0}", ex.Message);
                throw new ApiException(ex.Message);
            }
        }

        #endregion

        #region CRUD - R

        /// <summary>
        /// Gets Job by Id
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Returns Job</returns>
        /// <response code="200">Returns Job</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JobResponse), Status200OK)]
        public ApiResponse GetJobById([FromRoute] string id)
        {
            try
            {
                var result = _jobService.GetJobById(id);
                return new ApiResponse("Job data fetched Succesfully", result, Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetJobById - Exception: {0}", ex.Message);
                throw new ApiException(ex.Message);
            }
        }

        /// <summary>
        /// Gets Jobs
        /// </summary>
        /// <returns>Returns Jobs</returns>
        /// <response code="200">Returns Job</response>
        [HttpGet]
        [ProducesResponseType(typeof(JobResponse), Status200OK)]
        public ApiResponse GetJobs([FromQuery] JobFilter filter)
        {
            try
            {
                var result = _jobService.GetJobs(filter);
                return new ApiResponse("Jobs data fetched Succesfully", result, Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetJobs - Exception: {0}", ex.Message);
                throw new ApiException(ex.Message);
            }
        }

        #endregion
    }
}
