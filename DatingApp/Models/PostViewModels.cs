using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingApp.Models
{
    public class PostViewModels
    {

        public class PostIndexViewModel 
        {
           
            public virtual int CreatorId { get; set; }

            public virtual int RecieverId { get; set; }
 
            public string Content { get; set; }

            public DateTime DateTime { get; set; }

            public PostIndexViewModel(PostModel postModel)
            {
                CreatorId = postModel.CreatorId;
                RecieverId = postModel.RecieverId;
                Content = postModel.Content;
                DateTime = postModel.DateTime;
            }


            public PostIndexViewModel()
            {

            }

        }


    }
}