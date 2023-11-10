﻿namespace MovieReviewAPI.Models
{
    public class MovieListModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string? Genre { get; set; }
        public string? Country { get; set; }
        public string? Language { get; set; }
    }
}
