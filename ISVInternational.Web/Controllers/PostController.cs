using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ISVInternational.Core.ServiceRepos;
using ISVInternational.Entities.DTO;
using ISVInternational.Web.Helper;

namespace ISVInternational.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        [HttpGet]
        public ActionResult Index()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            AuthProcessor.ProcessAuthToken(claims);
            var posts = new List<UserDetailsDto>();
            posts = UserRepo.Instance.GetAllUsers(); 
            return View(posts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            AuthProcessor.ProcessAuthToken(claims);
            var user = AuthProcessor.GetMySessionObject();
            var post = new BlogPostDTO()
            {
                UserId = user.UserGuid
            };
            return View(post);
        }

        [HttpPost]
        public ActionResult Create(BlogPostDTO para)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            var user = AuthProcessor.ProcessAuthToken(claims);
            para.UserId = user.UserGuid;
            var status = BlogPostsRepo.Instance.CreatePost(para);
            return RedirectToAction("Index");
        }

        public ActionResult Posts(string id)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
                AuthProcessor.ProcessAuthToken(claims);
                var posts = BlogPostsRepo.Instance.GetAllPosts(Guid.Parse(id));
                return View(posts);
            //return View(new List<BlogPostDTO>());
        }

        public ActionResult View(int id)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            AuthProcessor.ProcessAuthToken(claims);
            var post = BlogPostsRepo.Instance.GetPost(id);
            post.Comments = BlogPostsRepo.Instance.GetComments(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Comment(CommentDTO comment)
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            var user = AuthProcessor.ProcessAuthToken(claims);
            comment.PostedUser = user.UserGuid;
            var post = BlogPostsRepo.Instance.PostComment(comment);
            return RedirectToAction("View", new {id = comment.PostId});
        }
        
        public ActionResult ShowCommentDialog(int id)
        {
            var model = new CommentDTO() {PostId = id}; 
            return PartialView("_Comment", model);
        }
    }
}