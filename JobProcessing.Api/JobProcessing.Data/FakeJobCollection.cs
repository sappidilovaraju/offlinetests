using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace JobProcessing.Data
{
    public class FakeJobCollection : IFakeJobCollection
    {
        #region Public Methods

        public JobModel Add(JobModel job)
        {
            var jobs = LoadDataFromFile();
            jobs.Add(job);
            UpdateDataToFile(jobs);
            return job;
        }

        public JobModel Get(string id)
        {
            var jobs = LoadDataFromFile();
            return jobs.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<JobModel> Get()
        {
            var jobs = LoadDataFromFile();
            return jobs;
        }

        public JobModel Update(JobModel job)
        {
            var jobs = LoadDataFromFile();
            var index = jobs.FindIndex(x => x.Id == job.Id);
            if (index == -1)
            {
                throw new KeyNotFoundException($"Job {job.Id} is not found");
            }

            jobs[index] = job;
            UpdateDataToFile(jobs);
            return job;
        }

        #endregion

        #region Private Methods

        private List<JobModel> LoadDataFromFile()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.Split("JobProcessing.Api")[0];
            path = path + "\\JobProcessing.Api\\data.json";
            if (!File.Exists(path))
            {
                return new List<JobModel>();
            }

            string data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<JobModel>>(data);
        }

        private void UpdateDataToFile(IEnumerable<JobModel> data)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.Split("JobProcessing.Api")[0];
            path = path + "\\JobProcessing.Api\\data.json";
            if (!File.Exists(path))
            {
                throw new KeyNotFoundException("File path is not found");
            }

            string content = JsonConvert.SerializeObject(data);
            File.WriteAllLines(path, new string[] { content });
        }

        #endregion
    }
}
