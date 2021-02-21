using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    [DataContract]
    public class Comments
    {
        private int id;
        private string commentText;
        private int userId;
        private int recipeId;
        private DateTime datetime;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string CommentText
        {
            get { return commentText; }
            set { commentText = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [DataMember]
        public int RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }

        [DataMember]
        public DateTime Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }

    }
}
