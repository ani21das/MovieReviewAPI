using MovieReviewAPI.Models.MovieList;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewAPI.Models.PlayList
{
    public class PlayListModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public List<MovieListModel> Movies { get; set; } = new List<MovieListModel>();
    }
}
