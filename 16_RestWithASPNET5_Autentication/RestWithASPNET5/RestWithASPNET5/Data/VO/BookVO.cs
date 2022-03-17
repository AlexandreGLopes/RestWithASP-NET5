using RestWithASPNET5.Hypermedia;
using RestWithASPNET5.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithASPNET5.Data.VO
{
    //Value Object do Book
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
