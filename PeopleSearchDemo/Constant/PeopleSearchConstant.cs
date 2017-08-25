using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearchDemo.Constant
{
    public class PeopleSearchConstant
    {
        // To load seed data (Location ~/SeedData)
        public static string ImageDirectoryPath = @"~/SeedData/Images/";
        public static string SeedDataXmlFilePath = @"~/SeedData/";
        public static string PeopleXmlFileName = @"People.xml";
        public static string PeopleNode = @"people";
        public static string DetailsNode = @"detail";
        public static string FirstNameNode = @"firstname";
        public static string LastNameNode = @"lastname";
        public static string AddressNode = @"address";
        public static string DateOfBirthNode = @"dataofbirth";
        public static string InterestsNode = @"interests";
        public static string GenderNode = @"gender";
        public static string ImageNameNode = @"imagename";

    }
}