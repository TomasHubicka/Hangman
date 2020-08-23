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
    public class GamescreenModel : PageModel
    {
        private GameLogic _gl;
        private SessionStorage<int[]> _iss;
        private SessionStorage<GameState> _gs;
        private SessionStorage<List<char>> _lcss;
        public string livesLeft { get; set; }
        public char[] currentWord { get; set; }
        public char[] wrongLetters { get; set; }
        public string category { get; set; }
        public GamescreenModel(GameLogic gl, SessionStorage<int[]> iss, SessionStorage<GameState> gs, SessionStorage<List<char>> lcss)
        {
            _gl = gl;
            _iss = iss;
            _gs = gs;
            _lcss = lcss;
        }

        public void OnGet()
        {
            GameState gs = _gs.LoadOrCreate("game");
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
        }
        public void OnGetAnimals()
        {
            _gl.Start(0, _iss, _gs, _lcss);
            GameState gs = _gs.LoadOrCreate("game");
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
            
        }
        public void OnGetVehicles()
        {
            _gl.Start(1, _iss, _gs, _lcss);
            GameState gs = _gs.LoadOrCreate("game");
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
        }
        public void OnGetSupplies()
        {
            _gl.Start(2, _iss, _gs, _lcss);
            GameState gs = _gs.LoadOrCreate("game");
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
        }
        public void OnGetCities()
        {
            _gl.Start(3, _iss, _gs, _lcss);
            GameState gs = _gs.LoadOrCreate("game");
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
        }
        public ActionResult OnPost(string letter)
        {
            GameState gs = _gs.LoadOrCreate("game");
            char[] letterArray = letter.ToCharArray();
            _gl.LetterCheck(letterArray[0], _iss, _gs, _lcss);
            livesLeft = ("HM-" + gs.LivesLeft + "life.svg");
            if (_gl.WordDoneCheck(_iss, _gs, _lcss))
            {
                return RedirectToPage("/Winscreen");
            }
            else if (gs.LivesLeft == 1)
            {
                return RedirectToPage("/Losescreen");
            }
            currentWord = _gl.WordSplitAndCover(_iss, _gs);
            wrongLetters = _gl.WrongLetters(_lcss);
            category = _gl.CategoryName(_gs);
            return null;
        }
    }
}