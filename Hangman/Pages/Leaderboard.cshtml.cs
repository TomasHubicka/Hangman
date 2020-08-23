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
    public class LeaderboardModel : PageModel
    {
        private DatabaseComms _dc;

        public LeaderboardModel(DatabaseComms dc)
        {
            _dc = dc;
        }

        public WordUserName[] WordUsers { get; set; }

        public void OnGet()
        {
            WordUsers = _dc.GetWordUsersNames();
        }
    }
}