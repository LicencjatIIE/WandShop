﻿using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Serializable]
    public class GameListViewModel
    {
        public List<Game> Games { get; set;}
    }
}