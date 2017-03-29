using Bond;

namespace GrpcSample.Shared
{
    [Schema]
    public class CustomRequest
    {
        [Id(0)]
        public int Payload { get; set; }
    }

    [Schema]
    public class CustomResponse
    {
        [Id(0)]
        public int Payload { get; set; }
    }
}