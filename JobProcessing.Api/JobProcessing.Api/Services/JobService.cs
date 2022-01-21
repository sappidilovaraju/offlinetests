using AutoMapper;
using JobProcessing.Api.DTO.Request;
using JobProcessing.Api.DTO.Response;
using JobProcessing.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobProcessing.Api.Services
{
    public class JobService : IJobService
    {
        #region Private Variables

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<JobService> _logger;
        private readonly IFakeJobCollection _fakeJobCollection;

        #endregion

        #region Constructor

        public JobService(IMediator mediator,
            IMapper mapper,
            ILogger<JobService> logger,
            IFakeJobCollection fakeJobCollection)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _fakeJobCollection = fakeJobCollection
                ?? throw new ArgumentNullException(nameof(fakeJobCollection));
        }

        #endregion

        #region Public Methods

        public async Task<JobResponse> CreateJob(CreateJobRequest job)
        {
            var jobModel = _mapper.Map<JobModel>(job);
            var result = _fakeJobCollection.Add(jobModel);
            //await _mediator.Publish(new JobCreatedEvent()
            //{
            //    JobId = result.Id
            //});
            return _mapper.Map<JobResponse>(result);
        }

        public JobResponse GetJobById(string id)
        {
            var result = _fakeJobCollection.Get(id);
            return _mapper.Map<JobResponse>(result);
        }

        public IEnumerable<JobResponse> GetJobs(JobFilter filter)
        {
            var jobs = _fakeJobCollection.Get();
            if (filter != null)
            {
                jobs = jobs.Where(x => filter.Status == null || x.Status == filter.Status);
            }

            return _mapper.Map<IEnumerable<JobResponse>>(jobs);
        }

        #endregion
    }
}
