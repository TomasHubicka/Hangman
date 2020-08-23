using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }
        public string guessWord { get; set; }
        public int Category { get; set; }
    }
}
