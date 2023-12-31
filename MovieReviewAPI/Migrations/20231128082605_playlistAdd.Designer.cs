﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieReviewAPI.Data.MovieContext;

#nullable disable

namespace MovieReviewAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231128082605_playlistAdd")]
    partial class playlistAdd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieReviewAPI.Models.MovieList.MovieListModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayListModelId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayListModelId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "USA",
                            Description = "A thief who enters the dreams of others to steal their secrets.",
                            Genre = "Sci-Fi, Action",
                            Language = "English",
                            Name = "Inception",
                            Rating = 8.8000000000000007,
                            ReleaseYear = 2010
                        },
                        new
                        {
                            Id = 2,
                            Country = "USA",
                            Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                            Genre = "Drama",
                            Language = "English",
                            Name = "The Shawshank Redemption",
                            Rating = 9.3000000000000007,
                            ReleaseYear = 1994
                        },
                        new
                        {
                            Id = 3,
                            Country = "USA",
                            Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                            Genre = "Crime, Drama",
                            Language = "English",
                            Name = "Pulp Fiction",
                            Rating = 8.9000000000000004,
                            ReleaseYear = 1994
                        },
                        new
                        {
                            Id = 4,
                            Country = "USA",
                            Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                            Genre = "Action, Crime, Drama",
                            Language = "English",
                            Name = "The Dark Knight",
                            Rating = 9.0,
                            ReleaseYear = 2008
                        },
                        new
                        {
                            Id = 5,
                            Country = "USA",
                            Description = "In German-occupied Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                            Genre = "Biography, Drama, History",
                            Language = "English",
                            Name = "Schindler's List",
                            Rating = 8.9000000000000004,
                            ReleaseYear = 1993
                        },
                        new
                        {
                            Id = 6,
                            Country = "USA",
                            Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal, and other historical events unfold from the perspective of an Alabama man with an IQ of 75.",
                            Genre = "Drama, Romance",
                            Language = "English",
                            Name = "Forrest Gump",
                            Rating = 8.8000000000000007,
                            ReleaseYear = 1994
                        },
                        new
                        {
                            Id = 7,
                            Country = "USA, Germany",
                            Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                            Genre = "Drama",
                            Language = "English",
                            Name = "Fight Club",
                            Rating = 8.8000000000000007,
                            ReleaseYear = 1999
                        },
                        new
                        {
                            Id = 8,
                            Country = "USA",
                            Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                            Genre = "Action, Sci-Fi",
                            Language = "English",
                            Name = "The Matrix",
                            Rating = 8.6999999999999993,
                            ReleaseYear = 1999
                        },
                        new
                        {
                            Id = 9,
                            Country = "USA, Germany",
                            Description = "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.",
                            Genre = "Adventure, Drama, War",
                            Language = "English, German, French, Italian",
                            Name = "Inglourious Basterds",
                            Rating = 8.3000000000000007,
                            ReleaseYear = 2009
                        },
                        new
                        {
                            Id = 10,
                            Country = "USA",
                            Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                            Genre = "Crime, Drama",
                            Language = "English, Italian, Latin",
                            Name = "The Godfather",
                            Rating = 9.1999999999999993,
                            ReleaseYear = 1972
                        },
                        new
                        {
                            Id = 11,
                            Country = "USA, UK, Malta, Morocco",
                            Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
                            Genre = "Action, Adventure, Drama",
                            Language = "English",
                            Name = "Gladiator",
                            Rating = 8.5,
                            ReleaseYear = 2000
                        },
                        new
                        {
                            Id = 12,
                            Country = "USA, Hong Kong",
                            Description = "An undercover cop and a mole in the police force attempt to identify each other while infiltrating an Irish gang in South Boston.",
                            Genre = "Crime, Drama, Thriller",
                            Language = "English, Cantonese",
                            Name = "The Departed",
                            Rating = 8.5,
                            ReleaseYear = 2006
                        },
                        new
                        {
                            Id = 13,
                            Country = "USA",
                            Description = "A young FBI cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.",
                            Genre = "Crime, Drama, Thriller",
                            Language = "English",
                            Name = "The Silence of the Lambs",
                            Rating = 8.5999999999999996,
                            ReleaseYear = 1991
                        },
                        new
                        {
                            Id = 14,
                            Country = "New Zealand, USA",
                            Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                            Genre = "Action, Adventure, Drama",
                            Language = "English, Quenya, Old English, Sindarin",
                            Name = "The Lord of the Rings: The Return of the King",
                            Rating = 8.9000000000000004,
                            ReleaseYear = 2003
                        },
                        new
                        {
                            Id = 15,
                            Country = "USA",
                            Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea, and by the co-founder who was later squeezed out of the business.",
                            Genre = "Biography, Drama",
                            Language = "English, French",
                            Name = "The Social Network",
                            Rating = 7.7000000000000002,
                            ReleaseYear = 2010
                        },
                        new
                        {
                            Id = 16,
                            Country = "USA",
                            Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
                            Genre = "Biography, Crime, Drama",
                            Language = "English",
                            Name = "The Wolf of Wall Street",
                            Rating = 8.1999999999999993,
                            ReleaseYear = 2013
                        },
                        new
                        {
                            Id = 17,
                            Country = "USA",
                            Description = "After John Nash, a brilliant but asocial mathematician, accepts secret work in cryptography, his life takes a turn for the nightmarish.",
                            Genre = "Biography, Drama",
                            Language = "English",
                            Name = "A Beautiful Mind",
                            Rating = 8.1999999999999993,
                            ReleaseYear = 2001
                        },
                        new
                        {
                            Id = 18,
                            Country = "Germany, USA",
                            Description = "A writer encounters the owner of an aging high-class hotel, who tells him of his early years serving as a lobby boy in the hotel's glorious years under an exceptional concierge.",
                            Genre = "Adventure, Comedy, Crime",
                            Language = "English, French",
                            Name = "The Grand Budapest Hotel",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2014
                        },
                        new
                        {
                            Id = 19,
                            Country = "USA",
                            Description = "A pragmatic paleontologist visiting an almost complete theme park is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.",
                            Genre = "Action, Adventure, Sci-Fi",
                            Language = "English, Spanish",
                            Name = "Jurassic Park",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 1993
                        },
                        new
                        {
                            Id = 20,
                            Country = "India",
                            Description = "Three inseparable childhood friends are just out of college. Nothing comes between them - until they each fall in love.",
                            Genre = "Comedy, Drama",
                            Language = "Hindi",
                            Name = "Dil Chahta Hai",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2001
                        },
                        new
                        {
                            Id = 21,
                            Country = "India",
                            Description = "Former wrestler Mahavir Singh Phogat trains young daughters Geeta and Babita to follow in his footsteps and become world-class grapplers.",
                            Genre = "Action, Biography, Drama",
                            Language = "Hindi",
                            Name = "Dangal",
                            Rating = 8.4000000000000004,
                            ReleaseYear = 2016
                        },
                        new
                        {
                            Id = 22,
                            Country = "India",
                            Description = "An alien on Earth loses his remote to a thief, who later finds it and discovers that the alien is harmless and just wants to go back to his planet.",
                            Genre = "Comedy, Drama, Fantasy",
                            Language = "Hindi",
                            Name = "PK",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2014
                        },
                        new
                        {
                            Id = 23,
                            Country = "India",
                            Description = "Two friends are searching for their long lost companion. They revisit their college days and recall the memories of their friend who inspired them to think differently, even as the rest of the world called them 'idiots'.",
                            Genre = "Comedy, Drama",
                            Language = "Hindi",
                            Name = "3 Idiots",
                            Rating = 8.4000000000000004,
                            ReleaseYear = 2009
                        },
                        new
                        {
                            Id = 24,
                            Country = "India",
                            Description = "The people of a small village in Victorian India stake their future on a game of cricket against their ruthless British rulers.",
                            Genre = "Adventure, Drama, Musical",
                            Language = "Hindi",
                            Name = "Lagaan: Once Upon a Time in India",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2001
                        },
                        new
                        {
                            Id = 25,
                            Country = "India",
                            Description = "Kabir Khan, the coach of the Indian women's national hockey team, faces several obstacles as he strives to bring success to the struggling team.",
                            Genre = "Drama, Family, Sport",
                            Language = "Hindi",
                            Name = "Chak De! India",
                            Rating = 8.1999999999999993,
                            ReleaseYear = 2007
                        },
                        new
                        {
                            Id = 26,
                            Country = "India",
                            Description = "A Delhi girl from a traditional family sets out on a solo honeymoon after her marriage gets canceled.",
                            Genre = "Adventure, Comedy, Drama",
                            Language = "Hindi",
                            Name = "Queen",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2013
                        },
                        new
                        {
                            Id = 27,
                            Country = "India",
                            Description = "A young man and woman - both of Indian descent but born and raised in Britain - fall in love during a trip to Switzerland. However, the girl's traditional father takes her back to India to fulfill a betrothal promise.",
                            Genre = "Drama, Romance",
                            Language = "Hindi",
                            Name = "Dilwale Dulhania Le Jayenge",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 1995
                        },
                        new
                        {
                            Id = 28,
                            Country = "India",
                            Description = "A successful Indian scientist returns to an Indian village to take his nanny to America with him and in the process rediscovers his roots.",
                            Genre = "Drama",
                            Language = "Hindi",
                            Name = "Swades",
                            Rating = 8.1999999999999993,
                            ReleaseYear = 2004
                        },
                        new
                        {
                            Id = 29,
                            Country = "India",
                            Description = "A series of mysterious events change the life of a blind pianist who now must report a crime that was actually never witnessed by him.",
                            Genre = "Crime, Thriller",
                            Language = "Hindi",
                            Name = "Andhadhun",
                            Rating = 8.3000000000000007,
                            ReleaseYear = 2018
                        },
                        new
                        {
                            Id = 30,
                            Country = "India",
                            Description = "A 16th century prince falls in love with a court dancer and battles with his emperor father.",
                            Genre = "Drama, Romance",
                            Language = "Hindi",
                            Name = "Mughal-E-Azam",
                            Rating = 8.1999999999999993,
                            ReleaseYear = 1960
                        },
                        new
                        {
                            Id = 31,
                            Country = "India",
                            Description = "A coming-of-age story about an aspiring street rapper from the slums of Mumbai.",
                            Genre = "Drama, Music",
                            Language = "Hindi",
                            Name = "Gully Boy",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2019
                        },
                        new
                        {
                            Id = 32,
                            Country = "India",
                            Description = "Yashvardhan Raichand lives a very wealthy lifestyle along with his wife, Nandini, and two sons, Rahul and Rohan. While Rahul has been adopted, Yashvardhan and Nandini treat him as their own. When their sons mature, they start to look for suitable brides for Rahul, and decide to get him married to a young woman named Naina.",
                            Genre = "Drama, Musical, Romance",
                            Language = "Hindi",
                            Name = "Kabhi Khushi Kabhie Gham",
                            Rating = 7.4000000000000004,
                            ReleaseYear = 2001
                        },
                        new
                        {
                            Id = 33,
                            Country = "India",
                            Description = "Three young people learn that love can neither be defined nor contained by society's norms of normal and abnormal.",
                            Genre = "Adventure, Comedy, Drama",
                            Language = "Hindi",
                            Name = "Barfi!",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2012
                        },
                        new
                        {
                            Id = 34,
                            Country = "India",
                            Description = "A young man returns to Kashmir after his father's disappearance to confront his uncle, whom he suspects of playing a role in his father's fate.",
                            Genre = "Action, Crime, Drama",
                            Language = "Hindi, Urdu",
                            Name = "Haider",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2014
                        },
                        new
                        {
                            Id = 35,
                            Country = "India",
                            Description = "A depressed wealthy businessman finds his life changing after he meets a spunky and care-free young woman.",
                            Genre = "Comedy, Drama, Romance",
                            Language = "Hindi, Panjabi, English",
                            Name = "Jab We Met",
                            Rating = 7.9000000000000004,
                            ReleaseYear = 2007
                        },
                        new
                        {
                            Id = 36,
                            Country = "India",
                            Description = "A developmentally disabled young man tries to continue the work his father did in communicating with extra-terrestrials from outer space, which leads to something miraculous and wonderful.",
                            Genre = "Drama, Fantasy, Romance",
                            Language = "Hindi",
                            Name = "Koi Mil Gaya",
                            Rating = 7.0999999999999996,
                            ReleaseYear = 2003
                        },
                        new
                        {
                            Id = 37,
                            Country = "India, USA",
                            Description = "An Indian Muslim man with Asperger's syndrome takes a challenge to speak to the President of the United States seriously and embarks on a cross-country journey.",
                            Genre = "Drama",
                            Language = "Hindi, English, Urdu",
                            Name = "My Name Is Khan",
                            Rating = 8.0,
                            ReleaseYear = 2010
                        },
                        new
                        {
                            Id = 38,
                            Country = "India",
                            Description = "Munna Bhai embarks on a journey with Mahatma Gandhi in order to fight against a corrupt property dealer.",
                            Genre = "Comedy, Drama, Romance",
                            Language = "Hindi, English",
                            Name = "Lage Raho Munna Bhai",
                            Rating = 8.0999999999999996,
                            ReleaseYear = 2006
                        },
                        new
                        {
                            Id = 39,
                            Country = "India",
                            Description = "After his wealthy family prohibits him from marrying the woman he is in love with, Devdas Mukherjee's life spirals further and further out of control as he takes up alcohol and a life of vice to numb the pain.",
                            Genre = "Drama, Musical, Romance",
                            Language = "Hindi",
                            Name = "Devdas",
                            Rating = 7.5999999999999996,
                            ReleaseYear = 2002
                        },
                        new
                        {
                            Id = 40,
                            Country = "India",
                            Description = "In the 1970s, Om, an aspiring actor, is murdered, but is immediately reincarnated into the present day. He attempts to discover the mystery of his demise and find Shanti, the love of his previous life.",
                            Genre = "Action, Comedy, Drama",
                            Language = "Hindi",
                            Name = "Om Shanti Om",
                            Rating = 6.7000000000000002,
                            ReleaseYear = 2007
                        });
                });

            modelBuilder.Entity("MovieReviewAPI.Models.PlayList.PlayListModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlayLists");
                });

            modelBuilder.Entity("ReviewModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieListModelId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Recommended")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieListModelId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MovieReviewAPI.Models.MovieList.MovieListModel", b =>
                {
                    b.HasOne("MovieReviewAPI.Models.PlayList.PlayListModel", null)
                        .WithMany("Movies")
                        .HasForeignKey("PlayListModelId");
                });

            modelBuilder.Entity("ReviewModel", b =>
                {
                    b.HasOne("MovieReviewAPI.Models.MovieList.MovieListModel", null)
                        .WithMany("Reviews")
                        .HasForeignKey("MovieListModelId");
                });

            modelBuilder.Entity("MovieReviewAPI.Models.MovieList.MovieListModel", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieReviewAPI.Models.PlayList.PlayListModel", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
