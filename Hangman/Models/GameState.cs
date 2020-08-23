using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class GameState
    {
        public string Word { get; set; }
        public int Category { get; set; }
        public int AmountFinished { get; set; }
        public int LivesLeft { get; set; }
    }
}
