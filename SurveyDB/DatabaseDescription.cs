using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace survey
{
    internal class DatabaseDescription
    {
        // Person Table
        public static string DATABASE_TABLE_Person = "Person";
        public static string PERSON_PersonId = "PersonId";
        public static string PERSON_FirstName = "FirstName";
        public static string PERSON_LastName = "LastName";

        // Survey Table
        public static string DATABASE_TABLE_Survey = "Survey";
        public static string SURVEY_SurveyId = "SurveyId";
        public static string SURVEY_PersonId = "PersonId";
        public static string SURVEY_Answer1 = "Answer1";
        public static string SURVEY_Answer2 = "Answer2";
    }
}
