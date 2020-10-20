namespace BFYOC
{
    // {
    //   "userId": "cc20a6fb-a91f-4192-874d-132493685376",
    //   "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
    //   "locationName": "Sample ice cream shop",
    //   "rating": 5,
    //   "userNotes": "I love the subtle notes of orange in this ice cream!"
    // }
    public class RatingInput
    {
        public string userId { get; set; }
        public string productId { get; set; }
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }

    // {
    //   "id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
    //   "userId": "cc20a6fb-a91f-4192-874d-132493685376",
    //   "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
    //   "timestamp": "2018-05-21 21:27:47Z",
    //   "locationName": "Sample ice cream shop",
    //   "rating": 5,
    //   "userNotes": "I love the subtle notes of orange in this ice cream!"
    // }
    public class RatingOutput
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }
        public string timestamp { get; set; }
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }

    }

}