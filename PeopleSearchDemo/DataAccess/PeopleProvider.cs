using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PeopleSearchDemo.Models;
using System.Threading.Tasks;

namespace PeopleSearchDemo.DataAccess
{
    public class PeopleProvider
    {
        private PeopleContext _db;
        public PeopleProvider(PeopleContext db)
        {
            _db = db;
        }

        //Add People
        public int Create(People people)
        {
            _db.Peoples.Add(people);
            _db.SaveChanges();

            return people.Id;
        }

        // Get people list by given fisrt or last name
        public async Task<List<People>> SearchByName(string name)
        {
            List<People> peoples =  await _db.Peoples
                                    .Where(p => p.FirstName.ToLower().Trim().Contains(name.ToLower().Trim())
                                        || p.LastName.ToLower().Trim().Contains(name.ToLower().Trim()))
                                    .Select(p => p).ToListAsync();

            //Simulate slow search
            var rand = new Random();
            var delay = rand.Next(50) * 100;
            await Task.Delay(delay);

            return peoples.ToList();
        }

        // Get record count from database
        public int RecordsCount()
        {
            int count = _db.Peoples
                         .Select(p => p).Count();

            return count;
        }

    }
}