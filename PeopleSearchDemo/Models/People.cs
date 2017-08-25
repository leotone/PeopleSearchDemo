using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearchDemo.Models
{
    public class People
    {
        //People data model
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Interests { get; set; }
        public byte[] Image { get; set; }
        public string Gender { get; set; }

        public People()
        {
            FirstName = default(string);
            LastName = default(string);
            DateOfBirth = default(DateTime);
            Address = default(string);
            Interests = default(string);
            Gender = default(string);
            Image = null;
        }

        public People(string firstName, string lastName, string address, string interests, DateTime dateOfBirth, string gender)
        {
            FirstName = default(string);
            LastName = default(string);
            DateOfBirth = default(DateTime);
            Address = default(string);
            Interests = default(string);
            Gender = default(string);
            Image = null;
        }

        public bool Equals(People people)
        {

            return FirstName.Equals(people.FirstName)
                && LastName.Equals(people.LastName)
                && Address.Equals(people.Address)
                && DateOfBirth.Equals(people.DateOfBirth)
                && Interests.Equals(people.Interests)
                && Gender.Equals(people.Gender)
                && Image.Equals(people.Image);
        }

    }
}