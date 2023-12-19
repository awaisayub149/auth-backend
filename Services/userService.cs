// <summary>
// Service class responsible for user-related operations, such as validation, retrieval, and creation.
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;

namespace webapi.Services;
public class userService
{
    private readonly IMongoCollection<Userlist> _userlistCollection;
    private readonly TokenService _tokenService;

    // <summary>
    // Initializes a new instance of the userService class.
    // <param name="mongoDBSettings">MongoDB configuration settings injected via options included: 
    // db connection and name. 
    // </param>
    public userService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userlistCollection = database.GetCollection<Userlist>(
            mongoDBSettings.Value.CollectionName
        );
        _tokenService = new TokenService("ADMIN125125125!@#"); // Replace with your actual secret key
    }

    // <summary>
    // Validates user credentials and generates a token upon successful validation.
    // <param name="email">User email.</param>
    // <param name="password">User password.</param>
    // <returns>JWT token if validation succeeds, null otherwise.</returns>
    public string ValidateField(string email, string password)
    {
        var user = this._userlistCollection.Find(x => x.Email == email && x.Password == password)
            .FirstOrDefault();

        if (user == null)
        {
            return null;
        }
        var token = _tokenService.GenerateToken(email);
        return token;
    }

    // <summary>
    // Retrieves a list of all users from the MongoDB collection.
    // <returns>List of Userlist objects.</returns>
    public List<Userlist> GetUser()
    {
        return _userlistCollection.Find(user => true).ToList();
    }

    // <summary>
    // Retrieves a specific user based on their email address.
    // <param name="uEmail">User email address.</param>
    // <returns>Userlist object corresponding to the provided email.</returns>
    public Userlist GetUser(string uEmail) =>
        _userlistCollection.Find<Userlist>(x => x.Email == uEmail).FirstOrDefault();

    // <summary>
    // Creates a new user in the MongoDB collection.
    public void CreateUser(string name, string email, string password)
    {
        var newUser = new Userlist
        {
            Name = name,
            Email = email,
            Password = password
        };

        _userlistCollection.InsertOne(newUser);
    }
}
