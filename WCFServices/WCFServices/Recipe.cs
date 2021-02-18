using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    [DataContract]
    public class Recipe
    {
        private int id;
        private string title;
        private string ingredients;
        private string method;
        private string image;
        private string category;
        private string otherdetails;
        private int likes;
        private int dislikes;
        private int userID;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        [DataMember]
        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        [DataMember]
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        [DataMember]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        [DataMember]
        public string Otherdetails
        {
            get { return otherdetails; }
            set { otherdetails = value; }
        }

        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        [DataMember]
        public int Likes
        {
            get { return likes; }
            set { likes = value; }
        }
        [DataMember]
        public int Dislikes
        {
            get { return dislikes; }
            set { dislikes = value; }
        }
    }

    
}
