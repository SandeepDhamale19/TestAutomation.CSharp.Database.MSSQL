using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Helpers.Assertions;
using TestAutomation.Framework.Helpers.Database;
using TestAutomation.Framework.Helpers.UI_Helpers;
using TestAutomation.MSSQLTests.Pages.Pages_Data;

namespace TestAutomation.MSSQLTests.StepDefinitions
{
    [Binding]
    public sealed class DatabaseTestsSteps : UIFramework
    {
        //private DBHelpers connectDB = new DBHelpers();
        readonly DBQueries dBQueries;
        public DatabaseTestsSteps()
        {
            dBQueries = Page<DBQueries>();
            DBHelpers.DBConnect(dBQueries.DbConnectionStringAvengers);
        }

        [Given(@"I query for Avengers Infromation")]
        public void GivenIQueryForAvengersInfromation()
        {
            var avengerFirstName = DBHelpers.ExecuteScalar(dBQueries.AvengerFirstName);
            ScenarioContext.Current.Remove("AvengerFirstName");
            ScenarioContext.Current.Add("AvengerFirstName", avengerFirstName);
        }

        [Given(@"I query for avenger (.*) Infromation")]
        public void GivenIQueryForAvengerInfromation(string avenger)
        {
            ScenarioContext.Current.Add("AvengerCharacterName", avenger);
            var avengerFirstName = DBHelpers.ExecuteScalar(dBQueries.AvengerFirstNameDynamic);
            ScenarioContext.Current.Remove("AvengerFirstName");
            ScenarioContext.Current.Add("AvengerFirstName", avengerFirstName);
        }


        [Then(@"I get required details from database table")]
        public void ThenIGetRequiredDetailsFromDatabaseTable()
        {
            string avengerFirstName = ScenarioContext.Current.Get<string>("AvengerFirstName");
            AssertHelpers.AssertEquals(avengerFirstName, "Steven");
        }

        [Given(@"I Insert reocrds for Avengers movies")]
        public void GivenIInsertReocrdsForAvengersMovies()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I query for Movie list for an Avenger by Character name")]
        public void GivenIQueryForMovieListForAnAvengerByCharacterName()
        {
            var avengerMovieList = DBHelpers.ExecuteQuery(dBQueries.QueryAvengerMovieList, dBQueries.DbConnectionStringAvengers);
            ScenarioContext.Current.Remove("AvengerFirstName");
            ScenarioContext.Current.Add("AvengerFirstName", avengerMovieList);
        }

        [Then(@"records should be available in Avenger's Moviebase")]
        public void ThenIGetListOfMoviesForGivenCharacterName()
        {
            int avengerMovieList = ScenarioContext.Current.Get<int>("AvengerFirstName");
            AssertHelpers.AssertEquals(avengerMovieList.ToString(), "1");
        }

        [Given(@"I query for Avengers Characteristics")]
        public void GivenIQueryForAvengersCharacteristics()
        {
            var avengerCharacteristics = DBHelpers.DataReader(dBQueries.QueryAvengerCharacteristics);
            ScenarioContext.Current.Remove("AvengerCharacteristics");
            ScenarioContext.Current.Add("AvengerCharacteristics", avengerCharacteristics);
        }

        [Then(@"I get characteristics for given character name")]
        public void ThenIGetCharacteristicsForGivenCharacterName()
        {
            SqlDataReader rdr = ScenarioContext.Current.Get<SqlDataReader>("AvengerCharacteristics");
            while (rdr.Read())
            {
                var eyeColor = rdr["Eye_Color"].ToString(); //string col1Value = rdr["ColumnOneName"].ToString();
                                                            // string col1Value = rdr[0].ToString();
                                                            //listCharacteristics.Add(myString);
            }
        }

        [Given(@"I query for Avengers Visible Characteristics")]
        public void GivenIQueryForAvengersVisibleCharacteristics()
        {
            var avengeVisisblerCharacteristics = DBHelpers.ShowDataInGridView(dBQueries.QueryAvengerCharacteristics, dBQueries.DbConnectionStringAvengers);
            ScenarioContext.Current.Remove("AvengerVisibleCharacteristics");
            ScenarioContext.Current.Add("AvengerVisibleCharacteristics", avengeVisisblerCharacteristics);
        }

        [Then(@"I get visible characteristics for given character name")]
        public void ThenIGetVisibleCharacteristicsForGivenCharacterName()
        {
            DataTable dt = ScenarioContext.Current.Get<DataTable>("AvengerVisibleCharacteristics");
            string eyeColorCaptainAmerica = dt.Rows[1][2].ToString(); //string eyeColorCaptainAmerica = dt.Rows[1]["Eye_Color"].ToString();
        }

        [Given(@"I Insert reocrds for Avengers movies (.*), (.*) and (.*) using Store Procedure")]
        public void GivenIInsertReocrdsForAvengersMoviesUsingStoreProcedure(int Id, string movieName,string yearOfRelease)        
        {
            Dictionary<string, dynamic> dictInserMovieRecords = new Dictionary<string, dynamic>();
            dictInserMovieRecords.Add("@Id", Id);
            dictInserMovieRecords.Add("@Movie_Name", movieName);
            dictInserMovieRecords.Add("@YearOfRelease", yearOfRelease);

            DBHelpers.ExecuteStoreProcedure(dBQueries.InsertAvengerMovies, dictInserMovieRecords);
        }

        [Then(@"records should be inserted in Avenger's Moviebase for (.*), (.*) and (.*)")]
        public void ThenRecordsShouldBeInsertedInAvengerSMoviebaseFor(int Id, string movieName, string yearOfRelease)
        {
            var dtMovieRecords = (DataTable)DBHelpers.ShowDataInGridView(dBQueries.GetMovieRecords, dBQueries.DbConnectionStringAvengers);

            int characterId = (int) dtMovieRecords.Rows[0]["Id"];
            string characterMovieName = dtMovieRecords.Rows[0]["Movie_Name"].ToString();
            DateTime characterYearOfRelease = (DateTime)dtMovieRecords.Rows[0]["Year_Of_Release"];

            AssertHelpers.AssertEquals(characterId.ToString(), Id.ToString());
            AssertHelpers.AssertEquals(characterMovieName, movieName);
            AssertHelpers.AssertEquals(characterYearOfRelease.Year.ToString(), yearOfRelease);
        }

    }
}
