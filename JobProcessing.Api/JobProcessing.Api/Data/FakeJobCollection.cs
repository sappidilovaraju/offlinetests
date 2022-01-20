using System.Collections.Generic;
using System.Linq;

namespace JobProcessing.Api.Data
{
    public class FakeJobCollection : IFakeJobCollection
    {
        #region Private Variables

        private List<JobModel> _jobs;

        #endregion

        #region Constructor

        public FakeJobCollection()
        {
            _jobs = new List<JobModel>();
        }

        #endregion

        #region Public Methods

        public JobModel Add(JobModel job)
        {
            _jobs.Add(job);
            return job;
        }

        public JobModel Get(string id)
        {
            return _jobs.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<JobModel> Get()
        {
            return _jobs;
        }

        public JobModel Update(JobModel job)
        {
            var index = _jobs.FindIndex(x => x.Id == job.Id);
            if (index == -1)
            {
                throw new KeyNotFoundException($"Job {job.Id} is not found");
            }

            _jobs[index] = job;
            return job;
        }

        #endregion
    }
}
