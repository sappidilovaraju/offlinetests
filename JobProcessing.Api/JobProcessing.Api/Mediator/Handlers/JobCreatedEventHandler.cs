using JobProcessing.Api.Mediator.Events;
using JobProcessing.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobProcessing.Api.Mediator.Handlers
{
    public class JobCreatedEventHandler : INotificationHandler<JobCreatedEvent>
    {
        #region Private Variables

        private readonly ILogger<JobCreatedEventHandler> _logger;
        private readonly IFakeJobCollection _fakeJobCollection;

        #endregion

        #region Constructor

        public JobCreatedEventHandler(ILogger<JobCreatedEventHandler> logger,
            IFakeJobCollection fakeJobCollection)
        {
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));
            _fakeJobCollection = fakeJobCollection
                ?? throw new ArgumentNullException(nameof(fakeJobCollection));
        }

        #endregion

        #region Handlers

        public async Task Handle(JobCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var pendingJob = _fakeJobCollection.Get()
                                        .Where(x => x.Id == notification.JobId)
                                        .FirstOrDefault();
                if (pendingJob != null)
                {
                    _logger.LogInformation($"Job {pendingJob.Id} is processing");
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    pendingJob.Output = pendingJob.Numbers.OrderBy(x => x);
                    watch.Stop();
                    pendingJob.ProccessingTime = watch.ElapsedMilliseconds;
                    pendingJob.Status = EnumJobStatus.Completed;
                    pendingJob.UpdatedDate = DateTime.Now;
                    _fakeJobCollection.Update(pendingJob);
                    _logger.LogInformation($"Job {pendingJob.Id} is processed");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Message handling failed, Error: {0}", ex.Message);
                throw;
            }
        }

        #endregion
    }
}
