using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.CfS.Library.Options
{
    public class CfsApiOptions
    {
        public Guid InstanceId { get; set; }
        public string EnrollmentId { get; set; } = default!;
        public QueryParams QueryParams { get; set; } = default!;

        public CfsApiOptions(Guid instanceId, string enrollmentId, IEnumerable<KeyValuePair<string, StringValues>>? queryParams)
        {
            InstanceId = instanceId;
            EnrollmentId = enrollmentId;

            if (queryParams is not null && queryParams.Any())
            {
                QueryParams = new QueryParams();
                
                foreach (var param in queryParams)
                {
                    var paramKey = param.Key;
                    var paramValue = param.Value;

                    switch (paramKey)
                    {
                        case "apply":
                            QueryParams.Apply = paramValue!;
                            break;
                        case "count":
                            if (bool.TryParse(paramValue, out bool countResult))
                            {
                                QueryParams.Count = countResult;
                            }
                            break;
                        case "expand":
                            QueryParams.Expand = paramValue!;
                            break;
                        case "filter":
                            QueryParams.Filter = paramValue!;
                            break;
                        case "orderby":
                            QueryParams.OrderBy = paramValue!;
                            break;
                        case "select":
                            QueryParams.Select = paramValue!;
                            break;
                        case "skip":
                            if (int.TryParse(paramValue, out int skipResult))
                            {
                                QueryParams.Skip = skipResult;
                            }
                            break;
                        case "top":
                            if (int.TryParse(paramValue, out int topResult))
                            {
                                QueryParams.Top = topResult;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public class QueryParams
    {
        public string Apply { get; set; } = default!;
        public bool Count { get; set; }
        public string Expand { get; set; } = default!;
        public string Filter { get; set; } = default!;
        public string OrderBy { get; set; } = default!;
        public string Select { get; set; } = default!;
        public int Skip { get; set; }
        public int Top { get; set; }
    }
}
