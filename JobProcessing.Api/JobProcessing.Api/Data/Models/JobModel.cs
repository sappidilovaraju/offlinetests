using System;
using System.Collections.Generic;

namespace JobProcessing.Api.Data
{
    public class JobModel
    {
        public JobModel()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
            Status = EnumJobStatus.Pending;
        }

        public string Id { get; set; }
        public IEnumerable<int> Numbers { get; set; }
        public IEnumerable<int> Output { get; set; }
        public double ProccessingTime { get; set; }
        public EnumJobStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public enum EnumJobStatus
    {
        Pending,
        Completed
    }
}
