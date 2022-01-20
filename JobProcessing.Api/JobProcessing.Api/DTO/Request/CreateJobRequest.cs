using FluentValidation;
using System.Collections.Generic;

namespace JobProcessing.Api.DTO.Request
{
    public class CreateJobRequest
    {
        public IEnumerable<int> Numbers { get; set; }
    }

    public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidator()
        {
            RuleFor(o => o.Numbers).NotNull();
        }
    }
}
