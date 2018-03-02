using jobTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobTracker.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for Appstatuses
                if (context.AppStatus.Any())
                {
                    return;   // DB has been seeded
                }

                var AppStatuses = new AppStatus[]
                {
                    new AppStatus {
                        AppStatusTitle = "Not Yet Applied"
                    },
                    new AppStatus {
                        AppStatusTitle = "Applied"
                    },
                    new AppStatus {
                        AppStatusTitle = "Interview Offered"
                    },
                    new AppStatus {
                        AppStatusTitle = "Interview Completed"
                    },
                    new AppStatus {
                        AppStatusTitle = "Job Offer Received"
                    },
                    new AppStatus {
                        AppStatusTitle = "Not Hired"
                    },
                    new AppStatus {
                        AppStatusTitle = "Declined Offer"
                    },
                    new AppStatus {
                        AppStatusTitle = "Accepeted Offer"
                    },
                     new AppStatus {
                        AppStatusTitle = "Interview Scheduled"
                    }
                };

                foreach (AppStatus i in AppStatuses)
                {
                    context.AppStatus.Add(i);
                }
                context.SaveChanges();
            }
        }

    }
}