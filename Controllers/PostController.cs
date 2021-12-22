using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Entities;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private static ClinicDBContext _dBContext;
        private CategoryModel _categoryModel;
        private PostModel _postModel;
        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
            _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
            _categoryModel = new CategoryModel(_dBContext);
            _postModel = new PostModel(_dBContext);
        }







        public int Test1(int itemsPerPage)
        {
            return itemsPerPage * 2;
        }


        public void Test1()
        {
            int pageLimit = 10;
            int y = Test1(pageLimit); // y = 

        }








        [HttpGet]
        public IEnumerable<Post> Get()
        {
            /*IEnumerable<Category> categories = _dBContext.Categories;
            IEnumerable<Post> posts = _dBContext.Posts;

            foreach (Post onePost in posts)
            {
                foreach (Category oneCat in categories)
                {
                    if(onePost.CategoryId == oneCat.Id)
                    {
                        onePost.Category = oneCat;
                        break;
                    }
                }
            }

            return posts.ToArray();*/

            return _dBContext.Posts.Include(p => p.Category).ToArray();

        }



        // https://localhost:44394/Post/3/2 
        // page=3 limit=2
        [HttpGet("{page}/{limit}")]
        public IEnumerable<Post> Get(int page, int limit) //TODO добавить categoryId
        {
            // return _dBContext.Posts.Include(p => p.Category).ToArray();

            return _dBContext.Posts
                   .OrderBy(p => p.Id)
                   .Skip((page - 1) * limit)
                   .Take(limit).ToArray();


        }

        // https://localhost:44394/post/count?categoryId=2
        [HttpGet("count")]
        public int Get(int categoryId)
        {
            if (categoryId == 0)
            {
                return _dBContext.Posts.Count();
            }
            return _dBContext.Posts
                   .Where(p => p.CategoryId == categoryId)
                   .Count();
        }



    }
}
