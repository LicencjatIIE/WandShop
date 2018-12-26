using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Player
    {
        [ForeignKey("PlayerPart")]
        public int PlayerId { get; set; }
        public string Login { get; set; }
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //For Ef
        public virtual PlayerPart PlayerPart { get; set; }
    }
}
