﻿using System;

namespace BandsApi.Models
{
    public class BandDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FoundedYearsAgo { get; set; }

        public string MainGenre { get; set; }

    }
}
