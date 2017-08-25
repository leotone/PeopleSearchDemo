using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PeopleSearchDemo.Models;
using PeopleSearchDemo.Constant;
using System.Xml.Linq;
using System.IO;
using PeopleSearchDemo.Helper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace PeopleSearchDemo.DataAccess
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("PeopleContext")
        {
            //Create Database
             Database.SetInitializer(new PeopleDBInitializer());
        }

        public class PeopleDBInitializer : CreateDatabaseIfNotExists<PeopleContext>
        {
            //Seed Data
            protected override void Seed(PeopleContext context)
            {
                try
                {
                    IList<People> defaultPeople = GetSeedPeople();

                    foreach (People people in defaultPeople)
                        context.Peoples.Add(people);

                    base.Seed(context);
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLogging(ex);
                    throw;
                }

            }

            //Get people information from resource file
            public List<People> GetSeedPeople()
            {
                // Virtual path (of a directory) on server which contains XML for persons' information.
                string seedDataXmlFilePath = HttpContext.Current.Server.MapPath(PeopleSearchConstant.SeedDataXmlFilePath);
                List<People> seedPeoples = GetPeopleFromXmlFile(seedDataXmlFilePath);

                return seedPeoples;
            }

            public List<People> GetPeopleFromXmlFile(string path)
            {
                List<People> peoples = new List<People>();

                // Load seed data file : ~/Resources/SeedData/People.xml
                XDocument xmlDoc = XDocument.Load(Path.Combine(path, PeopleSearchConstant.PeopleXmlFileName));

                // Virtual path (of a directory) on server which contains images for people.
                string imageDirectoryPath = HttpContext.Current.Server.MapPath(PeopleSearchConstant.ImageDirectoryPath);

                // Iterate over each <detail> tag in xmlDoc to get people information.
                foreach (var DetailNode in xmlDoc.Descendants(PeopleSearchConstant.DetailsNode))
                {
                    People people = new People();
                    people.FirstName = DetailNode.Element(PeopleSearchConstant.FirstNameNode).Value;
                    people.LastName = DetailNode.Element(PeopleSearchConstant.LastNameNode).Value;
                    people.Address = DetailNode.Element(PeopleSearchConstant.AddressNode).Value;
                    people.Interests = DetailNode.Element(PeopleSearchConstant.InterestsNode).Value;
                    people.DateOfBirth = Convert.ToDateTime(DetailNode.Element(PeopleSearchConstant.DateOfBirthNode).Value);
                    people.Gender = DetailNode.Element(PeopleSearchConstant.GenderNode).Value;

                    string imageName = DetailNode.Element(PeopleSearchConstant.ImageNameNode).Value;

                    // Virtual path of image file on server for the given <imagename>
                    string imagePath = Path.Combine(imageDirectoryPath, imageName);

                    people.Image = GetImageBytes(imagePath);

                    peoples.Add(people);
                }

                return peoples;
            }

            public byte[] GetImageBytes(string path)
            {
                FileInfo fileInfo = new FileInfo(path);
                byte[] data = File.ReadAllBytes(path);

                return data;
            }
        }

        public virtual DbSet<People> Peoples { get; set; }
    }
}
