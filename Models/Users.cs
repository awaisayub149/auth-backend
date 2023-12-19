using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace webapi.Models;
// Model representing user information stored in MongoDB
public class Userlist
{

    // Summary
    // BsonId attribute indicates this property is the document identifier
    // BsonRepresentation specifies the representation of the ObjectId
    // BsonElement attribute maps the property to the "Email",Name and Password fields in MongoDB
    // Summary

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Password")]
    public string Password { get; set; }
}