using Hangman.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public class DatabaseComms
    {
        readonly ApplicationDbContext _db;
        readonly IHttpContextAccessor _hca;
        readonly UserManager<IdentityUser<Guid>> _um;

        public Random r = new Random();
        public List<Word> words = new List<Word>();
        public Word[] wordsArray;
        public List<WordUser> wordUsers = new List<WordUser>();

        public DatabaseComms(ApplicationDbContext db, IHttpContextAccessor hca, UserManager<IdentityUser<Guid>> um)
        {
            _db = db;
            _hca = hca;
            _um = um;
        }

        public string randomWordFromSet(int category)
        {

            words = _db.Words.Where(c => c.Category == category).ToList();
            wordsArray = new Word[words.Count];
            wordUsers = _db.WordUsers.Where(u => u.UserId == _hca.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value).ToList();
            int i = 0;
            foreach (Word x in words)
            {
                foreach (WordUser y in wordUsers)
                {
                    if (x.Id != y.WordId)
                    {
                        wordsArray[i] = x;
                    }
                }
                i++;
            }
            words = wordsArray.ToList();
            int random = r.Next(0, words.Count);
            return wordsArray[random].guessWord;
        }
        public void AddWordUser(string word)
        {
            Word Word = _db.Words.Where(c => c.guessWord == word).FirstOrDefault();
            int wordId = Word.Id;
            string userId = _hca.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            _db.WordUsers.Add(new WordUser { UserId = userId, WordId = wordId});
            _db.SaveChanges();
        }
        public WordUser[] GetWordUsers()
        {
            
            wordUsers = _db.WordUsers.ToList();
            WordUser[] wordUsersArray = new WordUser[wordUsers.Count];
            int i = 0;
            foreach(WordUser x in wordUsers)
            {
                wordUsersArray[i] = x;
                i++;
            }
            return wordUsersArray;

        }
        public WordUserName[] GetWordUsersNames()
        {

            wordUsers = _db.WordUsers.ToList();
            WordUser[] wordUsersArray = new WordUser[wordUsers.Count];
            WordUserName[] wordUsersNamesArray = new WordUserName[wordUsers.Count];
            
            int i = 0;
            foreach (WordUser x in wordUsers)
            {
                wordUsersNamesArray[i] = new WordUserName { User = _um.Users.FirstOrDefault(u => u.Id.ToString() == x.UserId).UserName, Word = _db.Words.Where(w => w.Id == x.WordId).FirstOrDefault().guessWord };
                i++;
            }
            return wordUsersNamesArray;

        }
    }
}
