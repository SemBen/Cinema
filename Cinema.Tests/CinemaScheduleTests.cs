using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cinema.Tests
{
    public class CinemaScheduleTests
    {
        CinemaSchedule cinemaSchedule;
        List<Movie> movies = new List<Movie>()
            {
                new Movie(215, "LordOfTheRings3"),
                new Movie(210, "LordOfTheRings2"),
                new Movie(205, "LordOfTheRings1"),
                new Movie(105, "TheHobbit3"),
                new Movie(100, "TheHobbit2"),
                new Movie(95, "TheHobbit1"),
                new Movie(70, "Shrek3"),
                new Movie(65, "Shrek2"),
                new Movie(60, "Shrek1"),
                new Movie(25, "ShortFilm3"),
                new Movie(20, "ShortFilm2"),
                new Movie(15, "ShortFilm1")
            };

        [SetUp]
        public void Setup()
        {
            cinemaSchedule = CinemaSchedule.Create(movies);
        }

        [TestCaseSource(nameof(ExpectedSchedule))]
        public void CreateSchedule_WhenAllFilmsAreDifferent_ShouldCreateRightSchedule(Dictionary<DateTime, string> expected)
        {
            cinemaSchedule.CreateSchedule();

            CollectionAssert.AreEquivalent(expected, cinemaSchedule.ScheduleResult);
        }

        private static IEnumerable<object[]> ExpectedSchedule()
        {
            yield return new object[] { new Dictionary<DateTime, string>()
            {
                { new DateTime(2021, 6, 7, 10, 0, 0), "ShortFilm1"},
                { new DateTime(2021, 6, 7, 10, 15, 0), "ShortFilm2"},
                { new DateTime(2021, 6, 7, 10, 35, 0), "ShortFilm3"},
                { new DateTime(2021, 6, 7, 11, 0, 0), "Shrek1"},
                { new DateTime(2021, 6, 7, 12, 0, 0), "Shrek2"},
                { new DateTime(2021, 6, 7, 13, 5, 0), "Shrek3"},
                { new DateTime(2021, 6, 7, 14, 15, 0), "TheHobbit1"},
                { new DateTime(2021, 6, 7, 15, 50, 0), "TheHobbit2"},
                { new DateTime(2021, 6, 7, 17, 30, 0), "TheHobbit3"},
                { new DateTime(2021, 6, 7, 19, 15, 0), "LordOfTheRings1"},
                { new DateTime(2021, 6, 7, 22, 40, 0), "Shrek3"}
            }};
        }
    }
}