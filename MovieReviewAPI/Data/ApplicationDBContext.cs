using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<MovieListModel> Movies { get; set; }

        // MovieDbContext.cs
        // MovieDbContext.cs
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieListModel>().HasData(
                new MovieListModel
                {
                    Id = 1,
                    Name = "Inception",
                    Description = "A thief who enters the dreams of others to steal their secrets.",
                    ReleaseYear = 2010,
                    Rating = 8.8,
                    Genre = "Sci-Fi, Action",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 2,
                    Name = "The Shawshank Redemption",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    ReleaseYear = 1994,
                    Rating = 9.3,
                    Genre = "Drama",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 3,
                    Name = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    ReleaseYear = 1994,
                    Rating = 8.9,
                    Genre = "Crime, Drama",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 4,
                    Name = "The Dark Knight",
                    Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    ReleaseYear = 2008,
                    Rating = 9.0,
                    Genre = "Action, Crime, Drama",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 5,
                    Name = "Schindler's List",
                    Description = "In German-occupied Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    ReleaseYear = 1993,
                    Rating = 8.9,
                    Genre = "Biography, Drama, History",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 6,
                    Name = "Forrest Gump",
                    Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.",
                    ReleaseYear = 1994,
                    Rating = 8.8,
                    Genre = "Drama, Romance",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 7,
                    Name = "Fight Club",
                    Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                    ReleaseYear = 1999,
                    Rating = 8.8,
                    Genre = "Drama",
                    Country = "USA, Germany",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 8,
                    Name = "The Matrix",
                    Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    ReleaseYear = 1999,
                    Rating = 8.7,
                    Genre = "Action, Sci-Fi",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 9,
                    Name = "Inglourious Basterds",
                    Description = "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.",
                    ReleaseYear = 2009,
                    Rating = 8.3,
                    Genre = "Adventure, Drama, War",
                    Country = "USA, Germany",
                    Language = "English, German, French, Italian"
                },
                new MovieListModel
                {
                    Id = 10,
                    Name = "The Godfather",
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    ReleaseYear = 1972,
                    Rating = 9.2,
                    Genre = "Crime, Drama",
                    Country = "USA",
                    Language = "English, Italian, Latin"
                },
                new MovieListModel
                {
                    Id = 11,
                    Name = "Gladiator",
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                    ReleaseYear = 2000,
                    Rating = 8.5,
                    Genre = "Action, Adventure, Drama",
                    Country = "USA, UK, Malta, Morocco",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 12,
                    Name = "The Departed",
                    Description = "An undercover cop and a mole in the police force attempt to identify each other while infiltrating an Irish gang in South Boston.",
                    ReleaseYear = 2006,
                    Rating = 8.5,
                    Genre = "Crime, Drama, Thriller",
                    Country = "USA, Hong Kong",
                    Language = "English, Cantonese"
                },
                new MovieListModel
                {
                    Id = 13,
                    Name = "The Silence of the Lambs",
                    Description = "A young FBI cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.",
                    ReleaseYear = 1991,
                    Rating = 8.6,
                    Genre = "Crime, Drama, Thriller",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 14,
                    Name = "The Lord of the Rings: The Return of the King",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    ReleaseYear = 2003,
                    Rating = 8.9,
                    Genre = "Action, Adventure, Drama",
                    Country = "New Zealand, USA",
                    Language = "English, Quenya, Old English, Sindarin"
                },
                new MovieListModel
                {
                    Id = 15,
                    Name = "The Social Network",
                    Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea, and by the co-founder who was later squeezed out of the business.",
                    ReleaseYear = 2010,
                    Rating = 7.7,
                    Genre = "Biography, Drama",
                    Country = "USA",
                    Language = "English, French"
                },
                new MovieListModel
                {
                    Id = 16,
                    Name = "The Wolf of Wall Street",
                    Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
                    ReleaseYear = 2013,
                    Rating = 8.2,
                    Genre = "Biography, Crime, Drama",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 17,
                    Name = "A Beautiful Mind",
                    Description = "After John Nash, a brilliant but asocial mathematician, accepts secret work in cryptography, his life takes a turn for the nightmarish.",
                    ReleaseYear = 2001,
                    Rating = 8.2,
                    Genre = "Biography, Drama",
                    Country = "USA",
                    Language = "English"
                },
                new MovieListModel
                {
                    Id = 18,
                    Name = "The Grand Budapest Hotel",
                    Description = "A writer encounters the owner of an aging high-class hotel, who tells him of his early years serving as a lobby boy in the hotel's glorious years under an exceptional concierge.",
                    ReleaseYear = 2014,
                    Rating = 8.1,
                    Genre = "Adventure, Comedy, Crime",
                    Country = "Germany, USA",
                    Language = "English, French"
                },
                new MovieListModel
                {
                    Id = 19,
                    Name = "Jurassic Park",
                    Description = "A pragmatic paleontologist visiting an almost complete theme park is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.",
                    ReleaseYear = 1993,
                    Rating = 8.1,
                    Genre = "Action, Adventure, Sci-Fi",
                    Country = "USA",
                    Language = "English, Spanish"
                },
                new MovieListModel
                {
                    Id = 20,
                    Name = "Dil Chahta Hai",
                    Description = "Three inseparable childhood friends are just out of college. Nothing comes between them - until they each fall in love.",
                    ReleaseYear = 2001,
                    Rating = 8.1,
                    Genre = "Comedy, Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 21,
                    Name = "Dangal",
                    Description = "Former wrestler Mahavir Singh Phogat trains young daughters Geeta and Babita to follow in his footsteps and become world-class grapplers.",
                    ReleaseYear = 2016,
                    Rating = 8.4,
                    Genre = "Action, Biography, Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 22,
                    Name = "PK",
                    Description = "An alien on Earth loses his remote to a thief, who later finds it and discovers that the alien is harmless and just wants to go back to his planet.",
                    ReleaseYear = 2014,
                    Rating = 8.1,
                    Genre = "Comedy, Drama, Fantasy",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 23,
                    Name = "3 Idiots",
                    Description = "Two friends are searching for their long lost companion. They revisit their college days and recall the memories of their friend who inspired them to think differently, even as the rest of the world called them 'idiots'.",
                    ReleaseYear = 2009,
                    Rating = 8.4,
                    Genre = "Comedy, Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 24,
                    Name = "Lagaan: Once Upon a Time in India",
                    Description = "The people of a small village in Victorian India stake their future on a game of cricket against their ruthless British rulers.",
                    ReleaseYear = 2001,
                    Rating = 8.1,
                    Genre = "Adventure, Drama, Musical",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 25,
                    Name = "Chak De! India",
                    Description = "Kabir Khan, the coach of the Indian women's national hockey team, faces several obstacles as he strives to bring success to the struggling team.",
                    ReleaseYear = 2007,
                    Rating = 8.2,
                    Genre = "Drama, Family, Sport",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 26,
                    Name = "Queen",
                    Description = "A Delhi girl from a traditional family sets out on a solo honeymoon after her marriage gets canceled.",
                    ReleaseYear = 2013,
                    Rating = 8.1,
                    Genre = "Adventure, Comedy, Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 27,
                    Name = "Dilwale Dulhania Le Jayenge",
                    Description = "A young man and woman - both of Indian descent but born and raised in Britain - fall in love during a trip to Switzerland. However, the girl's traditional father takes her back to India to fulfill a betrothal promise.",
                    ReleaseYear = 1995,
                    Rating = 8.1,
                    Genre = "Drama, Romance",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 28,
                    Name = "Swades",
                    Description = "A successful Indian scientist returns to an Indian village to take his nanny to America with him and in the process rediscovers his roots.",
                    ReleaseYear = 2004,
                    Rating = 8.2,
                    Genre = "Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 29,
                    Name = "Andhadhun",
                    Description = "A series of mysterious events change the life of a blind pianist who now must report a crime that was actually never witnessed by him.",
                    ReleaseYear = 2018,
                    Rating = 8.3,
                    Genre = "Crime, Thriller",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 30,
                    Name = "Mughal-E-Azam",
                    Description = "A 16th century prince falls in love with a court dancer and battles with his emperor father.",
                    ReleaseYear = 1960,
                    Rating = 8.2,
                    Genre = "Drama, Romance",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 31,
                    Name = "Gully Boy",
                    Description = "A coming-of-age story about an aspiring street rapper from the slums of Mumbai.",
                    ReleaseYear = 2019,
                    Rating = 8.1,
                    Genre = "Drama, Music",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 32,
                    Name = "Kabhi Khushi Kabhie Gham",
                    Description = "Yashvardhan Raichand lives a very wealthy lifestyle along with his wife, Nandini, and two sons, Rahul and Rohan. While Rahul has been adopted, Yashvardhan and Nandini treat him as their own. When their sons mature, they start to look for suitable brides for Rahul, and decide to get him married to a young woman named Naina.",
                    ReleaseYear = 2001,
                    Rating = 7.4,
                    Genre = "Drama, Musical, Romance",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 33,
                    Name = "Barfi!",
                    Description = "Three young people learn that love can neither be defined nor contained by society's norms of normal and abnormal.",
                    ReleaseYear = 2012,
                    Rating = 8.1,
                    Genre = "Adventure, Comedy, Drama",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 34,
                    Name = "Haider",
                    Description = "A young man returns to Kashmir after his father's disappearance to confront his uncle, whom he suspects of playing a role in his father's fate.",
                    ReleaseYear = 2014,
                    Rating = 8.1,
                    Genre = "Action, Crime, Drama",
                    Country = "India",
                    Language = "Hindi, Urdu"
                },
                new MovieListModel
                {
                    Id = 35,
                    Name = "Jab We Met",
                    Description = "A depressed wealthy businessman finds his life changing after he meets a spunky and care-free young woman.",
                    ReleaseYear = 2007,
                    Rating = 7.9,
                    Genre = "Comedy, Drama, Romance",
                    Country = "India",
                    Language = "Hindi, Panjabi, English"
                },
                new MovieListModel
                {
                    Id = 36,
                    Name = "Koi Mil Gaya",
                    Description = "A developmentally disabled young man tries to continue the work his father did in communicating with extra-terrestrials from outer space, which leads to something miraculous and wonderful.",
                    ReleaseYear = 2003,
                    Rating = 7.1,
                    Genre = "Drama, Fantasy, Romance",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 37,
                    Name = "My Name Is Khan",
                    Description = "An Indian Muslim man with Asperger's syndrome takes a challenge to speak to the President of the United States seriously and embarks on a cross-country journey.",
                    ReleaseYear = 2010,
                    Rating = 8.0,
                    Genre = "Drama",
                    Country = "India, USA",
                    Language = "Hindi, English, Urdu"
                },
                new MovieListModel
                {
                    Id = 38,
                    Name = "Lage Raho Munna Bhai",
                    Description = "Munna Bhai embarks on a journey with Mahatma Gandhi in order to fight against a corrupt property dealer.",
                    ReleaseYear = 2006,
                    Rating = 8.1,
                    Genre = "Comedy, Drama, Romance",
                    Country = "India",
                    Language = "Hindi, English"
                },
                new MovieListModel
                {
                    Id = 39,
                    Name = "Devdas",
                    Description = "After his wealthy family prohibits him from marrying the woman he is in love with, Devdas Mukherjee's life spirals further and further out of control as he takes up alcohol and a life of vice to numb the pain.",
                    ReleaseYear = 2002,
                    Rating = 7.6,
                    Genre = "Drama, Musical, Romance",
                    Country = "India",
                    Language = "Hindi"
                },
                new MovieListModel
                {
                    Id = 40,
                    Name = "Om Shanti Om",
                    Description = "In the 1970s, Om, an aspiring actor, is murdered, but is immediately reincarnated into the present day. He attempts to discover the mystery of his demise and find Shanti, the love of his previous life.",
                    ReleaseYear = 2007,
                    Rating = 6.7,
                    Genre = "Action, Comedy, Drama",
                    Country = "India",
                    Language = "Hindi"
                }
                );
        }
    }
}

