using System.Collections.Generic;

namespace JobProcessing.Api.Data
{
    public interface IFakeJobCollection
    {
        JobModel Add(JobModel job);
        JobModel Get(string id);
        IEnumerable<JobModel> Get();
        JobModel Update(JobModel job);
    }
}
