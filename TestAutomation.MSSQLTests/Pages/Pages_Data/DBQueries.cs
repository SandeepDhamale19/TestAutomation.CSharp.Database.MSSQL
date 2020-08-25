using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Helpers.Json;
using TestAutomation.Framework.Helpers.Setup;

namespace TestAutomation.MSSQLTests.Pages.Pages_Data
{
    public class DBQueries : BasePage<DBQueries>
    {
        JsonHelper jHelper = new JsonHelper();

        private string dbConnectionStringAvengers = "DbConnectionStringAvengers";
        public string DbConnectionStringAvengers => jHelper.GetLocalData(dbConnectionStringAvengers, "DBQueries");

        private string queryAvengerFirstName = "QUERY_AvengerFirstName";
        public string AvengerFirstName => jHelper.GetLocalData(queryAvengerFirstName, "DBQueries");

        private string queryAvengerMovieList = "QUERY_AvengerMovieList";
        public string QueryAvengerMovieList => jHelper.GetLocalData(queryAvengerMovieList, "DBQueries");

        private string queryAvengerCharacteristics = "QUERY_AvengerCharacteristics";
        public string QueryAvengerCharacteristics => jHelper.GetLocalData(queryAvengerCharacteristics, "DBQueries");


        private string queryAvengerVisibleCharacteristics = "QUERY_AvengerVisibleCharacteristics";
        public string QueryAvengerVisibleCharacteristics => jHelper.GetLocalData(queryAvengerVisibleCharacteristics, "DBQueries");

        private string queryAvengerFirstNameDynamic = "QUERY_AvengerFirstNameDynamic";
        public string AvengerFirstNameDynamic => jHelper.GetLocalData(queryAvengerFirstNameDynamic, "DBQueries").Replace("[?]", ScenarioContext.Current.Get<string>("AvengerCharacterName"));

        private string spInsertAvengerMovies = "SP_InsertAvengerMovies";
        public string InsertAvengerMovies => jHelper.GetLocalData(spInsertAvengerMovies, "DBQueries");

        private string queryMovieRecords = "QUERY_MovieRecords";
        public string GetMovieRecords => jHelper.GetLocalData(queryMovieRecords, "DBQueries");
    }
}
