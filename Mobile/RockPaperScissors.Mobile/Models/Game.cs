using System;

namespace RockPaperScissors.Mobile.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public int HostScore { get; set; }
        public int GuestScore { get; set; }
        public int ScoreLimit { get; set; }
        public bool IsActive { get; set; }

        public Guid? HostId { get; set; }
        public User Host { get; set; }
        public Guid? GuestId { get; set; }
        public User Guest { get; set; }

        public GameResult? Result { get; set; }

        public bool HostTurnFinished { get; set; }
        public bool GuestTurnFinished { get; set; }
        public GameFigure HostLastFigure { get; set; }
        public GameFigure GuestLastFigure { get; set; }
    }

    public enum GameResult
    {
        HostWin,
        GuestWin
    }

    public enum GameFigure
    {
        Rock,
        Paper,
        Scissors
    }
}