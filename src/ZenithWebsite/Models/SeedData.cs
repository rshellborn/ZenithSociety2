//SeedData.cs 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebsite.Models;

namespace ZenithWebsite.Models
{
    public class SeedData
    {
        public static void Initialize(ZenithContext db)
        {

            if (!db.ApplicationRoles.Any())
            {
                db.ApplicationRoles.Add(new ApplicationRole
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    Description = "Administrator"
                });
                db.ApplicationRoles.Add(new ApplicationRole
                {
                    RoleId = 2,
                    RoleName = "Member",
                    Description = "Member"
                });
                db.ApplicationRoles.Add(new ApplicationRole
                {
                    RoleId = 3,
                    RoleName = "Guest",
                    Description = "Guest"
                });
            }

                if (!db.Activities.Any())
            {
                // Activity Seed Data
                db.Activities.Add(new Activity
                {
                    Description = "Senior's Golf Tournament",
                    CreationDate = new DateTime(2017, 03, 04)

                });
                db.Activities.Add(new Activity
                {
                    Description = "Leadership General Assembly Meeting",
                    CreationDate = new DateTime(2017, 03, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Youth Bowling Tournament",
                    CreationDate = new DateTime(2017, 03, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Young ladies cooking lessons",
                    CreationDate = new DateTime(2017, 04, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Youth craft leassons",
                    CreationDate = new DateTime(2017, 04, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Youth choir practice",
                    CreationDate = new DateTime(2017, 04, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Lunch",
                    CreationDate = new DateTime(2017, 05, 04)
                });

                db.Activities.Add(new Activity
                {
                    Description = "Pancake Breakfast",
                    CreationDate = new DateTime(2017, 05, 04)
                });

                db.SaveChanges();
                //...................................................
                // Events Seed Data
                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 1,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = true
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 2,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 3,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 4,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 5,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 6,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 7,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.Events.Add(new Event
                {
                    EventFrom = new DateTime(2017, 03, 04, 8, 30, 0),
                    EventTo = new DateTime(2017, 03, 04, 10, 20, 0),
                    EnteredBy = "johnny",
                    ActivityId = 8,
                    CreationDate = Convert.ToDateTime("2017/01/01"),
                    IsActive = false
                });

                db.SaveChanges();
            }
        }
    }
}
