using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateGameParamModel
    {
        public int CreateGameParamModelId { get; set; }
        [Required]
        [Range(0.2, 0.6, ErrorMessage = "Proszę podać liczbę z zakresu 0.2 do 0.6")]
        [Display(Name = "Podatek")]
        public double Tax { get; set; } = 0.4;
        [Required]
        [Range(0.2, 0.8, ErrorMessage = "Proszę podać liczbę z zakresu 0.2 do 0.8")]
        [Display(Name = "Dywidenda")]
        public double Dividend { get; set; } = 0.5;
        [Required]
        [Range(2000000, 3000000, ErrorMessage = "Proszę podać liczbę z zakresu 2000000 do 3000000")]
        [Display(Name = "Wkład własny")]
        public double OwnContribution { get; set; } = 2400000;
        [Required]
        [Range(800000, 1200000, ErrorMessage = "Proszę podać liczbę z zakresu 800000 do 1200000")]
        [Display(Name = "Kapitał obcy")]
        public double ForeignShares { get; set; } = 1000000;
        [Required]
        [Range(400000, 600000, ErrorMessage = "Proszę podać liczbę z zakresu 400000 do 600000")]
        [Display(Name = "Kredyt")]
        public double Loan { get; set; } = 600000;
        [Required]
        [Range(0.04, 0.12, ErrorMessage = "Proszę podać liczbę z zakresu 0.04 do 0.12")]
        [Display(Name = "Stopa kredytu")]
        public double InterestRate { get; set; } = 0.08;
        [Required]
        [Range(150000, 250000, ErrorMessage = "Proszę podać liczbę z zakresu 150000 do 250000")]
        [Display(Name = "Koszt zarządu")]
        public double ManagementCosts { get; set; } = 200000;
        [Required]
        [Range(5, 15, ErrorMessage = "Proszę podać liczbę z zakresu 5 do 15")]
        [Display(Name = "Koszt transportu poj. różdżki")]
        public double TransportCosts { get; set; } = 10;
    }
}