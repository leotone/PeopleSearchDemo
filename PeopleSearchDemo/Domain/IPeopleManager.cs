using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.ViewModel;

namespace PeopleSearchDemo.Domain
{
    public interface IPeopleManager
    {
        People FindPeople(PeopleViewModel peopleViewModel, byte[] imageBytes);
    }
}
