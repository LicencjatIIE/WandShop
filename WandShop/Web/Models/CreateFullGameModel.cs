using Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class CreateFullGameModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }
        
        [Required]
        [Range(3, 10, ErrorMessage = "Proszę podać liczbę z zakresu od 3 do 10")]
        [Display(Name = "Ilość rund")]
        public int MaxRounds { get; set; }

        [Required]
        [Range(2, 4, ErrorMessage = "Proszę podać liczbę z zakresu od 2 do 4")]
        [Display(Name = "Ilość graczy")]
        public int PlayersNumber { get; set; }

        [HiddenInput(DisplayValue = false)]
        public CreateGameParamModel CreateGameParamModel { get; set; }
        
        public Game ConvertToGame()
        {
            Game game = new Game();
            game.CurrentRound = 1;
            game.MaxRounds = MaxRounds;
            return game;
        }
    }
}