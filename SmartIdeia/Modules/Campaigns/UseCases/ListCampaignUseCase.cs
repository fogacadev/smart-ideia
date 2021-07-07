using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.UseCases
{
    public class ListCampaignUseCase
    {
        private readonly DatabaseContext context;
        public ListCampaignUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Campaign>> Execute(string research = "")
        {
            List<Campaign> campaigns = new List<Campaign>();
            if (!string.IsNullOrEmpty(research))
            {
                campaigns = await context
                    .Campaigns
                    .Where(c => c.Title.Contains(research)
                    || c.Description.Contains(research))
                    .OrderBy(c => c.Title)
                    .ToListAsync();
            }
            else
            {
                campaigns = await context
                    .Campaigns
                    .OrderBy(c => c.Title)
                    .ToListAsync();
            }
            return campaigns;
        }
    }
}
