using System;
using System.Collections.Generic;
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

        //For Ef
        public virtual PlayerPart PlayerPart { get; set; }
    }
}
