using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.UseCases
{
    public class FindCampaignUseCase
    {
        private readonly DatabaseContext context;
        public FindCampaignUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Campaign> Execute(long id)
        {
            var campaign = await context.Campaigns.Where(c => c.Id == id).FirstOrDefaultAsync();

            if(campaign == null)
            {
                throw new Exception("Campaign not exists");
            }

            return campaign;
        }
    }
}
