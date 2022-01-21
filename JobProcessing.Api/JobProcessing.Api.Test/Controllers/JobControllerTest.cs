using AutoMapper;
using JobProcessing.Api.Controllers;
using JobProcessing.Api.DTO.Request;
using JobProcessing.Api.DTO.Response;
using JobProcessing.Api.Infrastructure.Configs;
using JobProcessing.Api.Services;
using JobProcessing.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JobProcessing.Api.Test.Controllers
{
    public class JobControllerTest
    {
        #region Private Variables

        private readonly Mock<IFakeJobCollection> _fakeJobCollectionMock;
        private readonly JobController _controller;

        #endregion

        #region Constructor

        public JobControllerTest()
        {
            _fakeJobCollectionMock = new Mock<IFakeJobCollection>();

            var mediator = Mock.Of<IMediator>();
            var mapperProfile = new MappingProfileConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(configuration);
            var jobLogger = Mock.Of<ILogger<JobService>>();
            var jobService = new JobService(mediator, mapper, jobLogger, _fakeJobCollectionMock.Object);

            var controllerLogger = Mock.Of<ILogger<JobController>>();
            _controller = new JobController(jobService, controllerLogger);
        }

        #endregion

        #region Tests

        [Fact]
        public void Test_CreateJobRequest()
        {
            // Arrange
            var jobRequest = new CreateJobRequest()
            {
                Numbers = new List<int>() { 34, 5, 89, 10 }
            };

            // Act
            var response = _controller.CreateJob(jobRequest);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            var actualJob = JsonConvert.DeserializeObject<JobResponse>(JsonConvert.SerializeObject(response.Result));
            Assert.Equal(EnumJobStatus.Pending, actualJob.Status);
        }


        [Fact]
        public void Test_GetJobById()
        {
            // Arrange
            var job = new JobModel
            {
                Id = Guid.NewGuid().ToString(),
                Numbers = new List<int>() { 4, 2, 3, 6 },
                CreatedDate = DateTime.Now,
                Output = new List<int>() { 2, 3, 4, 6 },
                ProccessingTime = 1,
                Status = EnumJobStatus.Completed,
                UpdatedDate = DateTime.Now.AddMinutes(5),
            };
            _fakeJobCollectionMock.Setup(x => x.Get(job.Id)).Returns(job);

            // Act
            var response = _controller.GetJobById(job.Id);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            var actualJob = JsonConvert.DeserializeObject<JobResponse>(JsonConvert.SerializeObject(response.Result));
            Assert.Equal(job.Status, actualJob.Status);
            Assert.Equal(job.Output.Count(), actualJob.Output.Count());
        }

        [Fact]
        public void Test_GetJobsAll()
        {
            // Arrange
            var jobs = GetMockJobs();
            _fakeJobCollectionMock.Setup(x => x.Get()).Returns(jobs);

            // Act
            var response = _controller.GetJobs(new JobFilter { });

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            var actualJobs = JsonConvert.DeserializeObject<IEnumerable<JobResponse>>(JsonConvert.SerializeObject(response.Result));
            Assert.Equal(actualJobs.Count(), jobs.Count());
        }

        [Fact]
        public void Test_GetJobsPending()
        {
            // Arrange
            var jobs = GetMockJobs();
            var pendingJob = jobs.Where(x => x.Status == EnumJobStatus.Pending).FirstOrDefault();
            _fakeJobCollectionMock.Setup(x => x.Get()).Returns(jobs);

            // Act
            var response = _controller.GetJobs(new JobFilter { Status = EnumJobStatus.Pending });

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            var actualJobs = JsonConvert.DeserializeObject<IEnumerable<JobResponse>>(JsonConvert.SerializeObject(response.Result));
            Assert.Equal(actualJobs.Count(), 1);
            Assert.Equal(pendingJob.Status, actualJobs.FirstOrDefault().Status);
        }

        [Fact]
        public void Test_GetJobsCompleted()
        {
            // Arrange
            var jobs = GetMockJobs();
            var completedJob = jobs.Where(x => x.Status == EnumJobStatus.Completed).FirstOrDefault();
            _fakeJobCollectionMock.Setup(x => x.Get()).Returns(jobs);

            // Act
            var response = _controller.GetJobs(new JobFilter { Status = EnumJobStatus.Completed });

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            var actualJobs = JsonConvert.DeserializeObject<IEnumerable<JobResponse>>(JsonConvert.SerializeObject(response.Result));
            Assert.Equal(actualJobs.Count(), 1);
            Assert.Equal(completedJob.Status, actualJobs.FirstOrDefault().Status);
        }

        #endregion

        #region Private Methods

        private IEnumerable<JobModel> GetMockJobs()
        {
            return new List<JobModel>()
            {
                new JobModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Numbers = new List<int>() { 4, 2, 3, 6 },
                    CreatedDate = DateTime.Now,
                    Output = new List<int>() { 2, 3, 4, 6 },
                    ProccessingTime = 1,
                    Status = EnumJobStatus.Completed,
                    UpdatedDate = DateTime.Now.AddMinutes(5),
                },
                new JobModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Numbers = new List<int>() { 4, 2, 3, 6 },
                    CreatedDate = DateTime.Now,
                    Output = new List<int> { },
                    ProccessingTime = 0,
                    Status = EnumJobStatus.Pending,
                    UpdatedDate = null,
                }
            };
        }

        #endregion
    }
}
