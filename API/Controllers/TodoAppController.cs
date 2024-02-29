using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.Sqlite;
using Swashbuckle.AspNetCore.Annotations;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAppController : ControllerBase
    {
        private IConfiguration _configuration;
        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [SwaggerOperation(Summary = "Returns data on toxins derived from smoking.")]
        [HttpGet]
        [Route("GetToxinData")]
        public JsonResult GetToxinData()
        {
            string query = "select * from toxins";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetToxinsDescriptions")]
        public JsonResult GetToxinsDescriptions()
        {
            string query = "select * from toxinsDescriptions";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        [Route("AddUser")]
        public JsonResult AddUser([FromForm] int numberOfDays, [FromForm] string type, [FromForm] int quantity, [FromForm] int totalSpent, [FromForm] int totalIfInvested)
        {
            string query = "insert into userData (NumberOfDays, Type, Quantity, TotalSpent, TotalIfInvested) values (@numberOfDays, @type, @quantity, @totalSpent, @totalIfInvested)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@numberOfDays", numberOfDays);
                    myCommand.Parameters.AddWithValue("@type", type);
                    myCommand.Parameters.AddWithValue("@quantity", quantity);
                    myCommand.Parameters.AddWithValue("@totalSpent", totalSpent);
                    myCommand.Parameters.AddWithValue("@totalIfInvested", totalIfInvested);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added successfully");

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public JsonResult GetAllUsers()
        {
            string query = "select * from userData";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetAllUsersCalculatedData")]
        public JsonResult GetAllUsersCalculatedData()
        {
            string query = "SELECT 'Tobacco' AS Type, " +
                   "SUM(NumberOfDays) AS TotalNumberOfDays, " +
                   "SUM(Quantity) AS TotalQuantity, " +
                   "SUM(TotalSpent) AS TotalSpent, " +
                   "SUM(TotalIfInvested) AS TotalIfInvested " +
                   "FROM userData " +
                   "WHERE Type = 'Tobacco'";

            string query2 = "SELECT 'Electronic' AS Type, " +
                            "SUM(NumberOfDays) AS TotalNumberOfDays, " +
                            "SUM(Quantity) AS TotalQuantity, " +
                            "SUM(TotalSpent) AS TotalSpent, " +
                            "SUM(TotalIfInvested) AS TotalIfInvested " +
                            "FROM userData " +
                            "WHERE Type = 'Electronic'";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }

                using (SqliteCommand myCommand2 = new SqliteCommand(query2, myCon))
                {
                    myReader = myCommand2.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            List<object> resultList = new List<object>();

            foreach (DataRow row in table.Rows)
            {
                resultList.Add(new
                {
                    Type = row["Type"],
                    TotalNumberOfDays = row["TotalNumberOfDays"],
                    TotalQuantity = row["TotalQuantity"],
                    TotalSpent = row["TotalSpent"],
                    TotalIfInvested = row["TotalIfInvested"]
                });
            }

            return new JsonResult(resultList);

        }

        [HttpGet]
        [Route("GetAllUsersCalculatedDataByType/{type}")]
        public JsonResult GetAllUsersCalculatedDataByType(string type)
        {
            string query = "SELECT SUM(NumberOfDays) AS TotalNumberOfDays, " +
                  "SUM(Quantity) AS TotalQuantity, " +
                  "SUM(TotalSpent) AS TotalSpent, " +
                  "SUM(TotalIfInvested) AS TotalIfInvested " +
                  "FROM userData " +
                  $"WHERE Type = '{type}'";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            SqliteDataReader myReader;
            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("GetUserData{numberOfDays}/{type}/{quantity}/{priceToday}/{futureDays}")]
        public JsonResult GetUserData(int numberOfDays, string type, int quantity, int priceToday, int futureDays)
        {
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DBcon");
            string query = "";

            if (type == "Tobacco")
            {
                query = "SELECT Compound, Tobacco * @numberOfDays * @quantity * 20 * 12 AS totalConsumed FROM toxins";
            }
            else if (type == "Electronic")
            {
                query = "SELECT Compound, Electronic * @numberOfDays * @quantity * 600 AS totalConsumed FROM toxins";
            }
            else
            {
                return new JsonResult("Invalid type");
            }

            using (SqliteConnection myCon = new SqliteConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqliteCommand myCommand = new SqliteCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@numberOfDays", numberOfDays);
                    myCommand.Parameters.AddWithValue("@quantity", quantity);
                    SqliteDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            int historicalWeeks = (numberOfDays - futureDays) / 7;
            int futureWeeks = futureDays / 7;
            int totalWeeks = historicalWeeks + futureWeeks;
            double averageYearlyInflationRate = 0.03;
            double weeklyInflationRate = Math.Pow(1 + averageYearlyInflationRate, 1.0 / 52.0) - 1;
            double initialPrice = priceToday / Math.Pow(1 + weeklyInflationRate, historicalWeeks);
            double totalSpent = 0;
            double averageAnnualReturnRate = 0.05;
            double weeklyReturnRate = Math.Pow(1 + averageAnnualReturnRate, 1.0 / 52.0) - 1;
            double totalIfInvested = 0;

            foreach (int i in Enumerable.Range(0, totalWeeks - 1))
            {
                totalSpent += initialPrice * Math.Pow(1 + weeklyInflationRate, i) * quantity;
                totalIfInvested = (totalIfInvested + (initialPrice * Math.Pow(1 + weeklyInflationRate, i) * quantity)) * (1 + weeklyReturnRate);

            }

            DataRow histroicalSpendings = table.NewRow();
            histroicalSpendings["Compound"] = "historicalSpendings";
            histroicalSpendings["totalConsumed"] = totalSpent;
            table.Rows.Add(histroicalSpendings);

            DataRow ifInvested = table.NewRow();
            ifInvested["Compound"] = "totalIfInvested";
            ifInvested["totalConsumed"] = totalIfInvested;
            table.Rows.Add(ifInvested);

            DataRow totalCigarettes = table.NewRow();
            totalCigarettes["Compound"] = "totalCigarettes";
            totalCigarettes["totalConsumed"] = quantity * totalWeeks * 20;
            table.Rows.Add(totalCigarettes);

            if (type == "Tobacco")
            {
                DataRow totalLifeLost = table.NewRow();
                totalLifeLost["Compound"] = "totalLifeLost";
                totalLifeLost["totalConsumed"] = quantity * totalWeeks * 20 * 11 * 60;
                table.Rows.Add(totalLifeLost);

            }
            else
            {
                DataRow totalLifeLost = table.NewRow();
                totalLifeLost["Compound"] = "none";
                totalLifeLost["totalConsumed"] = 0;
                table.Rows.Add(totalLifeLost);
            }

            return new JsonResult(table);

        }
    }
}