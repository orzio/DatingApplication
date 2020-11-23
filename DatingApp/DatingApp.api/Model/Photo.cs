using System;

namespace DatingApp.api.Model
{
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //this is only to delete photos when user is deleted
        public User User { get; set; }

        public int UserID { get; set; }
    }
}