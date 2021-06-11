using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema
{
    public class CinemaSchedule
    {
        private List<Movie> _movies;
        private List<Movie> _usedMovies = new List<Movie>();
        private int _smallestDuration;
        private TimeSpan _timeLeft;
        private DateTime _currentTime;
        private DateTime _openTime = CinemaOpeningHours.openTime;
        private DateTime _closeTime = CinemaOpeningHours.closeTime;

        private Dictionary<DateTime, string> _schedule = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, string> ScheduleResult => _schedule;

        private CinemaSchedule(List<Movie> movies)
        {
            _smallestDuration = movies.Select(x => x.DurationInMinutes).Min();
            _movies = movies.OrderBy(x => x.DurationInMinutes).ToList();

            _timeLeft = _closeTime - _openTime;
            _currentTime = _openTime;
        }

        public static CinemaSchedule Create(List<Movie> movie)
        {
            if (movie != null && movie.Count > 0)
            {
                return new CinemaSchedule(movie);
            }
            else
            {
                throw new NullReferenceException("List with movies is empty");
            }
        }

        public void CreateSchedule()
        {
            _timeLeft = _closeTime - _currentTime;

            if (_timeLeft.TotalMinutes >= _smallestDuration && _movies.Count > 0)
            {
                var item = _movies.First();

                if (item.DurationInMinutes <= _timeLeft.TotalMinutes)
                {
                    _schedule.Add(_currentTime, item.Name);

                    _currentTime += TimeSpan.FromMinutes(item.DurationInMinutes);
                }

                _usedMovies.Add(item);
                _movies.Remove(item);
                CreateSchedule();
            }
            else if (_usedMovies.Any(x => x.DurationInMinutes < _timeLeft.TotalMinutes))
            {
                _movies = new List<Movie>(_usedMovies.OrderByDescending(x => x.DurationInMinutes));

                _usedMovies.Clear();
                CreateSchedule();
            }
        }

        public override bool Equals(object obj)
        {
            bool equal = false;
            CinemaSchedule cinemaSchedule = obj as CinemaSchedule;

            if (!(cinemaSchedule is null))
            {
                if (cinemaSchedule.ScheduleResult.Count == ScheduleResult.Count)
                {
                    equal = true;

                    foreach (var pair in cinemaSchedule.ScheduleResult)
                    {
                        string value;

                        if (ScheduleResult.TryGetValue(pair.Key, out  value))
                        {
                            if (value != pair.Value)
                            {
                                equal = false;
                                break;
                            }
                        }
                        else
                        {
                            equal = false;
                            break;
                        }
                    }
                }
            }

            return equal;
        }
    }
}
