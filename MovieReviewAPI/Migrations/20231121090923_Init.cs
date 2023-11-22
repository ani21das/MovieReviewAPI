using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommended = table.Column<bool>(type: "bit", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovieListModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieListModelId",
                        column: x => x.MovieListModelId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Country", "Description", "Genre", "Language", "Name", "Rating", "ReleaseYear" },
                values: new object[,]
                {
                    { 1, "USA", "A thief who enters the dreams of others to steal their secrets.", "Sci-Fi, Action", "English", "Inception", 8.8000000000000007, 2010 },
                    { 2, "USA", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Drama", "English", "The Shawshank Redemption", 9.3000000000000007, 1994 },
                    { 3, "USA", "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "Crime, Drama", "English", "Pulp Fiction", 8.9000000000000004, 1994 },
                    { 4, "USA", "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.", "Action, Crime, Drama", "English", "The Dark Knight", 9.0, 2008 },
                    { 5, "USA", "In German-occupied Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "Biography, Drama, History", "English", "Schindler's List", 8.9000000000000004, 1993 },
                    { 6, "USA", "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.", "Drama, Romance", "English", "Forrest Gump", 8.8000000000000007, 1994 },
                    { 7, "USA, Germany", "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.", "Drama", "English", "Fight Club", 8.8000000000000007, 1999 },
                    { 8, "USA", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", "Action, Sci-Fi", "English", "The Matrix", 8.6999999999999993, 1999 },
                    { 9, "USA, Germany", "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.", "Adventure, Drama, War", "English, German, French, Italian", "Inglourious Basterds", 8.3000000000000007, 2009 },
                    { 10, "USA", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Crime, Drama", "English, Italian, Latin", "The Godfather", 9.1999999999999993, 1972 },
                    { 11, "USA, UK, Malta, Morocco", "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.", "Action, Adventure, Drama", "English", "Gladiator", 8.5, 2000 },
                    { 12, "USA, Hong Kong", "An undercover cop and a mole in the police force attempt to identify each other while infiltrating an Irish gang in South Boston.", "Crime, Drama, Thriller", "English, Cantonese", "The Departed", 8.5, 2006 },
                    { 13, "USA", "A young FBI cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.", "Crime, Drama, Thriller", "English", "The Silence of the Lambs", 8.5999999999999996, 1991 },
                    { 14, "New Zealand, USA", "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "Action, Adventure, Drama", "English, Quenya, Old English, Sindarin", "The Lord of the Rings: The Return of the King", 8.9000000000000004, 2003 },
                    { 15, "USA", "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea, and by the co-founder who was later squeezed out of the business.", "Biography, Drama", "English, French", "The Social Network", 7.7000000000000002, 2010 },
                    { 16, "USA", "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.", "Biography, Crime, Drama", "English", "The Wolf of Wall Street", 8.1999999999999993, 2013 },
                    { 17, "USA", "After John Nash, a brilliant but asocial mathematician, accepts secret work in cryptography, his life takes a turn for the nightmarish.", "Biography, Drama", "English", "A Beautiful Mind", 8.1999999999999993, 2001 },
                    { 18, "Germany, USA", "A writer encounters the owner of an aging high-class hotel, who tells him of his early years serving as a lobby boy in the hotel's glorious years under an exceptional concierge.", "Adventure, Comedy, Crime", "English, French", "The Grand Budapest Hotel", 8.0999999999999996, 2014 },
                    { 19, "USA", "A pragmatic paleontologist visiting an almost complete theme park is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.", "Action, Adventure, Sci-Fi", "English, Spanish", "Jurassic Park", 8.0999999999999996, 1993 },
                    { 20, "India", "Three inseparable childhood friends are just out of college. Nothing comes between them - until they each fall in love.", "Comedy, Drama", "Hindi", "Dil Chahta Hai", 8.0999999999999996, 2001 },
                    { 21, "India", "Former wrestler Mahavir Singh Phogat trains young daughters Geeta and Babita to follow in his footsteps and become world-class grapplers.", "Action, Biography, Drama", "Hindi", "Dangal", 8.4000000000000004, 2016 },
                    { 22, "India", "An alien on Earth loses his remote to a thief, who later finds it and discovers that the alien is harmless and just wants to go back to his planet.", "Comedy, Drama, Fantasy", "Hindi", "PK", 8.0999999999999996, 2014 },
                    { 23, "India", "Two friends are searching for their long lost companion. They revisit their college days and recall the memories of their friend who inspired them to think differently, even as the rest of the world called them 'idiots'.", "Comedy, Drama", "Hindi", "3 Idiots", 8.4000000000000004, 2009 },
                    { 24, "India", "The people of a small village in Victorian India stake their future on a game of cricket against their ruthless British rulers.", "Adventure, Drama, Musical", "Hindi", "Lagaan: Once Upon a Time in India", 8.0999999999999996, 2001 },
                    { 25, "India", "Kabir Khan, the coach of the Indian women's national hockey team, faces several obstacles as he strives to bring success to the struggling team.", "Drama, Family, Sport", "Hindi", "Chak De! India", 8.1999999999999993, 2007 },
                    { 26, "India", "A Delhi girl from a traditional family sets out on a solo honeymoon after her marriage gets canceled.", "Adventure, Comedy, Drama", "Hindi", "Queen", 8.0999999999999996, 2013 },
                    { 27, "India", "A young man and woman - both of Indian descent but born and raised in Britain - fall in love during a trip to Switzerland. However, the girl's traditional father takes her back to India to fulfill a betrothal promise.", "Drama, Romance", "Hindi", "Dilwale Dulhania Le Jayenge", 8.0999999999999996, 1995 },
                    { 28, "India", "A successful Indian scientist returns to an Indian village to take his nanny to America with him and in the process rediscovers his roots.", "Drama", "Hindi", "Swades", 8.1999999999999993, 2004 },
                    { 29, "India", "A series of mysterious events change the life of a blind pianist who now must report a crime that was actually never witnessed by him.", "Crime, Thriller", "Hindi", "Andhadhun", 8.3000000000000007, 2018 },
                    { 30, "India", "A 16th century prince falls in love with a court dancer and battles with his emperor father.", "Drama, Romance", "Hindi", "Mughal-E-Azam", 8.1999999999999993, 1960 },
                    { 31, "India", "A coming-of-age story about an aspiring street rapper from the slums of Mumbai.", "Drama, Music", "Hindi", "Gully Boy", 8.0999999999999996, 2019 },
                    { 32, "India", "Yashvardhan Raichand lives a very wealthy lifestyle along with his wife, Nandini, and two sons, Rahul and Rohan. While Rahul has been adopted, Yashvardhan and Nandini treat him as their own. When their sons mature, they start to look for suitable brides for Rahul, and decide to get him married to a young woman named Naina.", "Drama, Musical, Romance", "Hindi", "Kabhi Khushi Kabhie Gham", 7.4000000000000004, 2001 },
                    { 33, "India", "Three young people learn that love can neither be defined nor contained by society's norms of normal and abnormal.", "Adventure, Comedy, Drama", "Hindi", "Barfi!", 8.0999999999999996, 2012 },
                    { 34, "India", "A young man returns to Kashmir after his father's disappearance to confront his uncle, whom he suspects of playing a role in his father's fate.", "Action, Crime, Drama", "Hindi, Urdu", "Haider", 8.0999999999999996, 2014 },
                    { 35, "India", "A depressed wealthy businessman finds his life changing after he meets a spunky and care-free young woman.", "Comedy, Drama, Romance", "Hindi, Panjabi, English", "Jab We Met", 7.9000000000000004, 2007 },
                    { 36, "India", "A developmentally disabled young man tries to continue the work his father did in communicating with extra-terrestrials from outer space, which leads to something miraculous and wonderful.", "Drama, Fantasy, Romance", "Hindi", "Koi Mil Gaya", 7.0999999999999996, 2003 },
                    { 37, "India, USA", "An Indian Muslim man with Asperger's syndrome takes a challenge to speak to the President of the United States seriously and embarks on a cross-country journey.", "Drama", "Hindi, English, Urdu", "My Name Is Khan", 8.0, 2010 },
                    { 38, "India", "Munna Bhai embarks on a journey with Mahatma Gandhi in order to fight against a corrupt property dealer.", "Comedy, Drama, Romance", "Hindi, English", "Lage Raho Munna Bhai", 8.0999999999999996, 2006 },
                    { 39, "India", "After his wealthy family prohibits him from marrying the woman he is in love with, Devdas Mukherjee's life spirals further and further out of control as he takes up alcohol and a life of vice to numb the pain.", "Drama, Musical, Romance", "Hindi", "Devdas", 7.5999999999999996, 2002 },
                    { 40, "India", "In the 1970s, Om, an aspiring actor, is murdered, but is immediately reincarnated into the present day. He attempts to discover the mystery of his demise and find Shanti, the love of his previous life.", "Action, Comedy, Drama", "Hindi", "Om Shanti Om", 6.7000000000000002, 2007 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieListModelId",
                table: "Reviews",
                column: "MovieListModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
