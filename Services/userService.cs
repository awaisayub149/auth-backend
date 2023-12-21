using Microsoft.Extensions.Options;
using MongoDB.Driver;
using webapi.Models;
using BCrypt.Net;


namespace webapi.Services;

public class userService
{
    private readonly IMongoCollection<Userlist> _userlistCollection;
    private readonly TokenService _tokenService;

    public userService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userlistCollection = database.GetCollection<Userlist>(
            mongoDBSettings.Value.CollectionName
        );
        _tokenService = new TokenService("ADMIN125125125!@#"); // Replace with your actual secret key
    }

    public string ValidateField(string email, string password)
    {
        var user = this._userlistCollection.Find(x => x.Email == email).FirstOrDefault();

        if (user == null)
        {
            return null; // User not found
        }

        // Validate the password using BCrypt
        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            // Password is valid, generate token
            var token = _tokenService.GenerateToken(email);
            return token;
        }

        // Password is invalid
        return null;
    }

    public List<Userlist> GetUser()
    {
        // Token validation succeeded, proceed to retrieve user information
        return _userlistCollection.Find(user => true).ToList();
    }

    public Userlist GetUser(string uEmail) =>
        _userlistCollection.Find<Userlist>(x => x.Email == uEmail).FirstOrDefault();

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
