using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows.Models.UserControlModels.Anime
{
    public class Anime
    {
        private string title;
        private string description;
        private byte rating;

        public byte Rating
        {
            get { return rating; }
            set { rating = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public Anime(string title, string description, byte rating)
        {
            Title = title;
            Description = description;
            Rating = rating;
        }

    }
}
