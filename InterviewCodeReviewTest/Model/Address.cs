namespace InterviewCodeReviewTest.Model
{
    public class Address
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public override string ToString() => $"{HouseNumber} , {Street} , {City} , {PostalCode}";
    }
}
