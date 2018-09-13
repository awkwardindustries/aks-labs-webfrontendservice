using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRequest.WebFrontEndService.Models
{
    public enum FeedbackLevel
    {
        Info,
        Warning,
        Error
    }

    public class Feedback
    {
        public FeedbackLevel Level { get; set; }
        public string Message { get; set; }

        public Feedback() { }

        public Feedback(FeedbackLevel level, string message)
        {
            this.Level = level;
            this.Message = message;
        }

        public string GetDisplayClass()
        {
            switch(this.Level)
            {
                case FeedbackLevel.Error:
                    return "alert alert-danger";
                case FeedbackLevel.Warning:
                    return "alert alert-warning";
                case FeedbackLevel.Info:
                default:
                    return "alert alert-dark";
            }
        }
    }
}
