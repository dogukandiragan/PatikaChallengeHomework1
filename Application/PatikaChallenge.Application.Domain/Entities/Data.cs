using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaChallenge.Application.Domain.Entities
{
    public class Data
    {
        public string appId { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; }
        public string pinStatus { get; set; }
        public string securityQuestionStatus { get; set; }

        public string authMode { get; set; }

        public string userToken { get; set; }
        public string encryptionKey { get; set; }

        public string challengeId { get; set; }

        public PinDetails pinDetails { get; set; }

        public SecurityQuestionDetails securityQuestionDetails { get; set; }
    }
    public class PinDetails
    {
        public int FailedAttempts { get; set; }
    }

    public class SecurityQuestionDetails
    {
        public int FailedAttempts { get; set; }
    }


}
