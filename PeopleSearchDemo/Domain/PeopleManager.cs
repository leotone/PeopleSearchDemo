using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.ViewModel;
using PeopleSearchDemo.DataAccess;
using PeopleSearchDemo.Helper;
using System.Threading.Tasks;

namespace PeopleSearchDemo.Domain
{
    public class PeopleManager
    {
        PeopleContext _db;
        PeopleService _peopleService;

        public PeopleManager()
        {
            _db = new PeopleContext();
            _peopleService = new PeopleService(_db);
        }

        //Create people
        public int Create(PeopleViewModel peopleViewModel, byte[] imageBytes)
        {
            try
            {
                People people = FindPeople(peopleViewModel, imageBytes);

                return _peopleService.Create(people);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogging(ex);
                throw ex;
            }
        }

        //Find People
        public People FindPeople(PeopleViewModel peopleViewModel, byte[] imageBytes)
        {
            try
            {
                if (string.IsNullOrEmpty(peopleViewModel.FirstName) && string.IsNullOrEmpty(peopleViewModel.LastName))
                {
                    return null;
                }
                else
                {
                    var people = new People();

                    people.FirstName = peopleViewModel.FirstName;
                    people.LastName = peopleViewModel.LastName;
                    people.Address = peopleViewModel.Address;
                    people.DateOfBirth = peopleViewModel.DateOfBirth;
                    people.Image = imageBytes;
                    people.Interests = peopleViewModel.Interests;
                    people.Gender = peopleViewModel.Gender;
                    return people;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogging(ex);
                throw ex;
            }
        }

        // Find list of people by given name.
        public async Task<List<PeopleViewModel>> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    List<People> peoples = await _peopleService.Search(name);
                    List<PeopleViewModel> personViewModelList = PopulatePeopleViewModelList(peoples);

                    return personViewModelList;
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLogging(ex);
                    throw;
                }
            }

            return new List<PeopleViewModel>();
        }

        // Populate people list view
        public static List<PeopleViewModel> PopulatePeopleViewModelList(List<People> peoples)
        {
            List<PeopleViewModel> peopleViewModelList = new List<PeopleViewModel>();

            foreach (People people in peoples)
            {
                PeopleViewModel peopleViewModel = new PeopleViewModel();
                peopleViewModel.FirstName = people.FirstName;
                peopleViewModel.LastName = people.LastName;
                peopleViewModel.Address = people.Address;
                peopleViewModel.DateOfBirth = people.DateOfBirth;
                peopleViewModel.Interests = people.Interests;
                peopleViewModel.Gender = people.Gender;
                double diff = DateTime.Now.Subtract(people.DateOfBirth).Days / (365.25 / 12);
                peopleViewModel.Age = (float)Math.Round(diff / 12, 2);

                if (people.Image != null)
                {
                    // Convert byte[] to image
                    var base64 = Convert.ToBase64String(people.Image);
                    string imgSrc = String.Format("data:image/jpeg;base64,{0}", base64);

                    peopleViewModel.Image = imgSrc;
                }

                peopleViewModelList.Add(peopleViewModel);
            }

            return peopleViewModelList;
        }

        // Seed data in database
        public string SeedData()
        {
            try
            {
                if (!_peopleService.RecordsCount().Equals(null))
                {
                    int recordsCount = _peopleService.RecordsCount();
                    return "Data Seed Successfuly! Record Count :  " + recordsCount.ToString() + ".";
                }
                else
                {
                    return "Error Occured while seeding data. Please try again with correct connectionstring configurations";
                }

            }
            catch (Exception ex)
            {
                LogHelper.ErrorLogging(ex);
                return String.Format("{0}{1}", "Error Occured while seeding data. Please try again with correct connectionstring configurations", ex.Message);
            }
        }

    }


    public class PeopleService
    {
        PeopleProvider _provider;

        public PeopleService(PeopleContext db)
        {
            _provider = new PeopleProvider(db);
        }

        public int Create(People people)
        {
            if (people != null)
            {
                if (string.IsNullOrEmpty(people.FirstName) && string.IsNullOrEmpty(people.LastName))

                    return -1;
                else

                    return _provider.Create(people);
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<People>> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return await _provider.SearchByName(name);
            else
                return null;
        }

        public int RecordsCount()
        {
            return _provider.RecordsCount();
        }

    }

}