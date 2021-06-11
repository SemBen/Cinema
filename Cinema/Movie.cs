namespace Cinema
{
    public class Movie
    {
        public int DurationInMinutes { get; set; }
        public string Name { get; set; }

        public Movie(int durationInMinutes, string name)
        {
            DurationInMinutes = durationInMinutes;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            bool equal = false;
            Movie movie = obj as Movie;

            if (!(movie is null))
            {
                if (DurationInMinutes == movie.DurationInMinutes && Name == movie.Name)
                {
                    equal = true;
                }
            }

            return equal;
        }
    }
}
