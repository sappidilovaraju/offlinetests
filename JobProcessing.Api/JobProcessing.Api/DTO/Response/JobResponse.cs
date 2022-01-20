using JobProcessing.Api.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace JobProcessing.Api.DTO.Response
{
    public class JobResponse
    {
        public string Id { get; set; }
        public IEnumerable<int> Output { get; set; }
        public double ProccessingTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumJobStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
