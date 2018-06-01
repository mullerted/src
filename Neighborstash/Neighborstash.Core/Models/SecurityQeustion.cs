using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Neighborstash.Core.Models
{
    public class SecurityQeustion
    {
        [BsonId]
        public string Username { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }


    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}