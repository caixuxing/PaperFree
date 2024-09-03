using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Dto.Response.BorrowDto
{

   public class BorrowDto
    {

        public string id { get; set; }
        public string name { get; set; }

        public DateTime dateTime { get; set; }

        public string LastModifierId { get; set; }
    }
}
