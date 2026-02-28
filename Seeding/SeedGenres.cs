using Zubair_Zunairah_HW3.DAL;
using Zubair_Zunairah_HW3.Models;
using System.Text;

namespace Zubair_Zunairah_HW3.Seeding
{
    public static class SeedGenres
    {
        public static void SeedAllGenres(AppDbContext db)
        {
            // check to see if all the genres have already been added
            if (db.Genres.Count() == 13)
            {
                //exit the program - we don't need to do any of this
                NotSupportedException ex = new NotSupportedException("Genre record count is already 13!");
                throw ex;
            }
            Int32 intGenresAdded = 0;
            try
            {
                //Create a list of languages
                List<Genre> Genres = new List<Genre>();

                Genre g1 = new Genre() { GenreName = "Adventure" };
                Genres.Add(g1);

                Genre g2 = new Genre() { GenreName = "Contemporary Fiction" };
                Genres.Add(g2);

                Genre g3 = new Genre() { GenreName = "Fantasy" };
                Genres.Add(g3);

                Genre g4 = new Genre() { GenreName = "Historical Fiction" };
                Genres.Add(g4);

                Genre g5 = new Genre() { GenreName = "Horror" };
                Genres.Add(g5);

                Genre g6 = new Genre() { GenreName = "Humor" };
                Genres.Add(g6);

                Genre g7 = new Genre() { GenreName = "Mystery" };
                Genres.Add(g7);

                Genre g8 = new Genre() { GenreName = "Poetry" };
                Genres.Add(g8);

                Genre g9 = new Genre() { GenreName = "Romance" };
                Genres.Add(g9);

                Genre g10 = new Genre() { GenreName = "Science Fiction" };
                Genres.Add(g10);

                Genre g11 = new Genre() { GenreName = "Shakespeare" };
                Genres.Add(g11);

                Genre g12 = new Genre() { GenreName = "Suspense" };
                Genres.Add(g12);

                Genre g13 = new Genre() { GenreName = "Thriller" };
                Genres.Add(g13);

                foreach (Genre genreToAdd in Genres)
                {
                    Genre dbGenre = db.Genres.FirstOrDefault(g => g.GenreName == genreToAdd.GenreName);
                    if (dbGenre == null)
                    {
                        db.Genres.Add(genreToAdd);
                        db.SaveChanges();
                        intGenresAdded += 1;
                    }
                }
            }
            catch
            {
                String msg = "Genres Added: " + intGenresAdded.ToString();
                throw new InvalidOperationException(msg);
            }
        }
    }
}
