using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PeopleSearchDemo.ViewModel
{
    public class PeopleViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Interests { get; set; }
        public string Gender { get; set; }
        public float Age { get; set; }

        public PeopleViewModel()
        {

        }

        public PeopleViewModel(string firstName, string lastName, DateTime dateOfBirth, string address, string image, string interests, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Address = address;
            Image = image;
            Interests = interests;
            Gender = gender;
        }

    }
}