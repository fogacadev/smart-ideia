using SmartIdeia.Src.Modules.Accounts.Entities;
using SmartIdeia.Src.Modules.ActionPlans.Entities;
using SmartIdeia.Src.Modules.Activities.Entities;
using SmartIdeia.Src.Modules.Authors.Entities;
using SmartIdeia.Src.Modules.Campaigns.Entities;
using SmartIdeia.Src.Modules.Entries.Entities;
using SmartIdeia.Src.Modules.IdeaRating.Entities;
using SmartIdeia.Src.Modules.Themes.Entities;
using System;
using System.Collections.Generic;

namespace SmartIdeia.Modules.Ideas.Entities
{
    public class Idea
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string What { get; set; }
        public string Why { get; set; }
        public string Where { get; set; }
        public string How { get; set; }
        public string Status { get; set; }
        public Theme Theme { get; set; }
        public long ThemeId { get; set; }

        public Campaign Campaign { get; set; }
        public long? CampaignId { get; set; }

        //Action plans
        public List<ActionPlan> ActionPlans { get; set; }

        //Activitys
        public List<Activity> Activities { get; set; }

        //Coauthors
        public List<Author> Coauthors { get; set; }
        public bool IsCollective { get; set; }

        //Monitoring
        public bool Measurable { get; set; }
        public string Unit { get; set; }
        public decimal Goal { get; set; }
        public decimal GoalAchievement { get; set; }
        public List<Entry> Entries { get; set; }


        //gamefication
        public decimal GeneratedPoints  { get; set; }
        public decimal AwardInPoints { get; set; }
        public List<IdeaRating> IdeaRatings { get; set; }


        public User CreatedBy { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
