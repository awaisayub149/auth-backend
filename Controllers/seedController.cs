using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IMongoCollection<Userlist> _userCollection;

        // Initialize the SeedController with a reference to the MongoDB user collection
        public SeedController(IMongoDatabase database, IOptions<MongoDBSettings> mongoDBSettings)
        {
            // Use the MongoDBSettings options to get the collection name from appsettings.json
            _userCollection = database.GetCollection<Userlist>(mongoDBSettings.Value.CollectionName);
        }

        [HttpPost]
        public IActionResult SeedData()
        {
            // Check if data already exists in the user collection
            if (_userCollection.CountDocuments(FilterDefinition<Userlist>.Empty) > 0)
            {
                return BadRequest("Data already seeded.");
            }

            // Seed dummy user data using the DummyData class
            var dummyUser = DummyData.GetDummyUser();
            _userCollection.InsertOne(dummyUser);

            return Ok("Data seeded successfully.");
        }
    }
}