using Amazon.DynamoDBv2.DataModel;

namespace NotesApp.Complete.Model
{
    [DynamoDBTable("notes")]
    public class Note
    {
        [DynamoDBHashKey("id")]
        public int Id { get; set; }

        [DynamoDBPropertyAttribute("username")]
        public string Username { get; set; }

        [DynamoDBPropertyAttribute("content")]
        public string Content { get; set; }
    }
}