﻿namespace TenderApp.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int TenderId { get; set; }
        public Tender Tender { get; set; } = null!;

        public int WinnerId { get; set; }
        public User Winner { get; set; } = null!;

        public string Details { get; set; } = null!;
    }
}
