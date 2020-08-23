using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public class GameLogic
    {
        private DatabaseComms _dc;
        public GameLogic(DatabaseComms dc)
        {
            _dc = dc;
        }

        public void Start(int category, SessionStorage<int[]> _iss, SessionStorage<GameState> _gs, SessionStorage<List<char>> _lcss)
        {
            string word = _dc.randomWordFromSet(category);
            int[] emptyArray = new int[word.Length];
            _iss.Save("CC", emptyArray);
            GameState gs = new GameState { AmountFinished = 0, Category = category, LivesLeft = 8, Word = word };
            _gs.Save("game", gs);
            List<char> emptyStringList = new List<char>();
            _lcss.Save("wrongLetters", emptyStringList);
        }
        public void LetterCheck(char letter, SessionStorage<int[]> _iss, SessionStorage<GameState> _gs, SessionStorage<List<char>> _lcss)
        {
            int[] correctlyChosen = _iss.LoadOrCreate("CC");
            GameState gs = _gs.LoadOrCreate("game");
            string word = RemoveDiacritics(gs.Word.ToUpper());
            char[] charArray = word.ToCharArray();
            string strletter = RemoveDiacritics(letter.ToString());
            char[] letterarr = strletter.ToUpper().ToCharArray();
            letter = letterarr[0];
            int i = 0;
            bool wrong = true;
            foreach (char x in charArray)
            {
                if (letter == x)
                {
                    correctlyChosen[i] = 1;
                    wrong = false;
                }

                i++;
            }
            if (wrong == true)
            {
                List<char> wrongLetters = _lcss.LoadOrCreate("wrongLetters");
                wrongLetters.Add(letter);
                _lcss.Save("wrongLetters", wrongLetters);
                gs.LivesLeft--;
            }
            _gs.Save("game", gs);
            _iss.Save("CC", correctlyChosen);
        }
        public bool WordDoneCheck(SessionStorage<int[]> _iss, SessionStorage<GameState> _gs, SessionStorage<List<char>> _lcss)
        {
            int[] correctlyChosen = _iss.LoadOrCreate("CC");
            GameState gs = _gs.LoadOrCreate("game");
            string word = gs.Word;
            int correctAmount = 0;
            foreach (int y in correctlyChosen)
            {
                if (y == 1)
                {
                    correctAmount++;
                }
            }
            if (word.Length == correctAmount)
            {
                int[] emptyArray = { };
                _iss.Save("CC", emptyArray);
                List<char> emptyCharList = new List<char>();
                _lcss.Save("wrongLetters", emptyCharList);
                gs.AmountFinished++;
                _gs.Save("game", gs);
                _dc.AddWordUser(word);
                return true;
            }
            return false;

        }
        public char[] WordSplitAndCover(SessionStorage<int[]> _iss, SessionStorage<GameState> _gs)
        {
            GameState gs = _gs.LoadOrCreate("game");
            char[] currentWord = gs.Word.ToCharArray();
            int[] correctlyChosen = _iss.LoadOrCreate("CC");
            char[] coveredWord = new char[gs.Word.Length];
            int x = 0;
            foreach(char i in currentWord)
            {
                if(correctlyChosen[x] == 1)
                {
                    coveredWord[x] = currentWord[x];
                }
                else
                {
                    coveredWord[x] = char.Parse("_");
                }
                x++;
            }
            return coveredWord;
        }
        public char[] WrongLetters(SessionStorage<List<char>> _lcss)
        {
            char[] wrongLetters = _lcss.LoadOrCreate("wrongLetters").ToArray();
            char[] wrongLettersEight = new char[8];
            int y = 0;
            foreach (char i in wrongLetters)
            {
                wrongLettersEight[y] = i;
                y++;
            }
            char[] wrongLettersShow = new char[8];

            for (int x = 0; x<8; x++)
            {
                if (wrongLettersEight[x] != '\0')
                {
                    wrongLettersShow[x] = wrongLettersEight[x];
                }
                else
                {
                    wrongLettersShow[x] = char.Parse("_");
                }
            }
            return wrongLettersShow;
        }
        public string RemoveDiacritics(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        public string CategoryName(SessionStorage<GameState> _gs)
        {
            GameState gs = _gs.LoadOrCreate("game");
            if(gs.Category == 0)
            {
                return "ZVÍŘATA";
            }
            else if(gs.Category == 1)
            {
                return "VOZIDLA";
            }
            else if (gs.Category == 2)
            {
                return "ŠKOLNÍ POTŘEBY";
            }
            else if(gs.Category == 3)
            {
                return "MĚSTA";
            }
            else
            {
                return "Error";
            }
        }
    }
}
