﻿using Hangman.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public class SessionStorage<T>
    {
        readonly ISession _session;

        public SessionStorage(IHttpContextAccessor hca)
        {
            _session = hca.HttpContext.Session;
        }

        public T LoadOrCreate(string key)
        {
            T result = _session.Get<T>(key);
            if (typeof(T).IsClass && result == null) result = (T)Activator.CreateInstance(typeof(T));
            return result;
        }

        public void Save(string key, T data)
        {
            _session.Set(key, data);
        }
    }
}
