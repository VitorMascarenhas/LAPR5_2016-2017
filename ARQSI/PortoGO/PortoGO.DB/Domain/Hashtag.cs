using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    public class Hashtag
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public string UserId { get; set; }

        public User User { get; private set; }

        public int PointOfInterestId { get; set; }

        public PointOfInterest PointOfInterest { get; set; }

        public Hashtag()
        {

        }

        public Hashtag(string tag, int pointOfInterestId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            this.Tag = tag;
            this.UserId = userId;
            this.PointOfInterestId = pointOfInterestId;
        }

        public Hashtag(string tag, PointOfInterest pointOfInterest, User user)
        {
            this.Tag = tag;

            if(pointOfInterest == null)
            {
                throw new ArgumentNullException("pointOfInterest");
            }

            if(user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.UserId = user.Id;
            this.User = user;

            this.PointOfInterestId = pointOfInterest.Id;
            this.PointOfInterest = PointOfInterest;
        }
    }
}
