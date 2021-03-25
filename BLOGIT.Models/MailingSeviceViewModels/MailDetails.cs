using System.Collections.Generic;

namespace BLOGIT.Models
{ 
    public class MailDetails
    {
        public List<string> Recievers { get; set; }

        public string MessageTitle { get; set; }

        public string MessageBody { get; set; }
    }
}
