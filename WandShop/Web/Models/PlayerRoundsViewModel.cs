using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class PlayerRoundsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public List<PlayerRoundStruct> PlayerRoundStructs{ get; }
        public PlayerRoundsViewModel(List<PlayerRound> prs)
        {
            PlayerRoundStructs = new List<PlayerRoundStruct>();
            foreach (var item in prs)
            {
                PlayerRoundStructs.Add(new PlayerRoundStruct(item.PlayerRoundId, item.PlayerRoundNumber));
            }
        }
    }
    public struct PlayerRoundStruct
    {
        public int PlayerRoundId { get; set; }
        public int PlayerRoundNumber { get; set; }
        public PlayerRoundStruct(int id, int number)
        {
            PlayerRoundId = id;
            PlayerRoundNumber = number;
        }
    }

}