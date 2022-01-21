using JobProcessing.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobProcessing.Worker
{
    public class JobProcessingWorker : BackgroundService
    {
        #region Private Variables

        private readonly ILogger<JobProcessingWorker> _logger;
        private readonly IFakeJobCollection _fakeJobCollection;

        #endregion

        #region Constructors

        public JobProcessingWorker(ILogger<JobProcessingWorker> logger,
            IFakeJobCollection fakeJobCollection)
        {
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _fakeJobCollection = fakeJobCollection
                ?? throw new ArgumentNullException(nameof(fakeJobCollection));
        }

        #endregion

        #region Public Methods

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var pendingJob = _fakeJobCollection.Get()
                                        .Where(x => x.Status == EnumJobStatus.Pending)
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
                catch (Exception ex)
                {
                    _logger.LogError($"ExecuteAsync - {ex.Message}");
                }
            }
        }

        #endregion
    }
}
