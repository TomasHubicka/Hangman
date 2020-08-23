using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangman.Models;
using Hangman.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hangman
{
    public class LosescreenModel : PageModel
    {
        private SessionStorage<GameState> _gs;

        public LosescreenModel(SessionStorage<GameState> gs)
        {
            _gs = gs;
        }
        public string Word { get; set; }
        public void OnGet()
        {
            GameState gs = _gs.LoadOrCreate("game");
            Word = gs.Word;
        }
    }
}