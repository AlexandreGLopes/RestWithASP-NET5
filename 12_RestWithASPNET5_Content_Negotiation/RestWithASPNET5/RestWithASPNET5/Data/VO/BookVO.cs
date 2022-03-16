using System;

namespace RestWithASPNET5.Data.VO
{
    //Value Object do Book
    public class BookVO
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
