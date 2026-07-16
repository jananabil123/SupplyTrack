namespace SupplyTrack
{
    public class SupplyRequest
    {
        public int Id { get; set; }

        public string RequesterName { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Draft;

        public List<RequestLine> Lines { get; set; } = new List<RequestLine>();
        public void Submit()
        {
            if (Status != RequestStatus.Draft)
            {
                Console.WriteLine("Only draft requests can be submitted.");
                return;
            }

            Status = RequestStatus.Submitted;
            Console.WriteLine("Request submitted successfully.");
        }
        public void Approve()
        {
            if (Status != RequestStatus.Submitted)
            {
                Console.WriteLine("Only submitted requests can be approved.");
                return;
            }

            Status = RequestStatus.Approved;
            Console.WriteLine("Request approved successfully.");
        }
        public void Reject()
        {
            if (Status != RequestStatus.Submitted)
            {
                Console.WriteLine("Only submitted requests can be rejected.");
                return;
            }

            Status = RequestStatus.Rejected;
            Console.WriteLine("Request rejected.");
        }
        public void Fulfill()
        {
            if (Status != RequestStatus.Approved)
            {
                Console.WriteLine("Only approved requests can be fulfilled.");
                return;
            }

            Status = RequestStatus.Fulfilled;
            Console.WriteLine("Request fulfilled successfully.");
        }

    }
}