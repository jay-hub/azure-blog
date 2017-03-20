using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ISVInternational.Ef;
using ISVInternational.Entities.DTO;
using ISVInternational.Entities.Entities;

namespace ISVInternational.Core.ServiceRepos
{

    public class BlogPostsRepo
    {
        private BlogContext _blogContext;
        private static BlogPostsRepo _instance;
        private BlogPostsRepo()
        {
            _blogContext = new BlogContext();
        }
        public static BlogPostsRepo Instance => _instance ?? (_instance = new BlogPostsRepo());

        public List<BlogPostDTO> GetAllPosts(Guid user)
        {
            try
            {
                var data = (from post in _blogContext.BlogPosts
                    where post.UserId == user
                    select new BlogPostDTO()
                    {
                        Id = post.Id,
                        UserId = post.UserId,
                        BlogTitle = post.BlogTitle,
                        CreatedDate = post.CreatedDate,
                        IsArchived = post.IsArchived,
                        IsPosted = post.IsPosted,
                        Post = post.Post
                    }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<BlogPostDTO>();
        }

        public BlogPostDTO GetPost(int id)
        {
            try
            {
                var data = (from post in _blogContext.BlogPosts
                    where post.Id == id
                    select new BlogPostDTO()
                    {
                        Id = post.Id,
                        UserId = post.UserId,
                        BlogTitle = post.BlogTitle,
                        CreatedDate = post.CreatedDate,
                        IsArchived = post.IsArchived,
                        IsPosted = post.IsPosted,
                        Post = post.Post
                    }).SingleOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new BlogPostDTO();
        }

        public bool CreatePost(BlogPostDTO para)
        {
            try
            {
                var post = new BlogPost()
                {
                    UserId = para.UserId,
                    BlogTitle = para.BlogTitle,
                    Post = para.Post,
                    CreatedDate = DateTime.Now,
                    IsArchived = 0,
                    IsPosted = 1,
                    PrivacyType = 1
                };

                _blogContext.BlogPosts.Add(post);
                _blogContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool PostComment(CommentDTO comm)
        {
            try
            {
                var cm = new Comment();
                cm.IsReported = comm.PostId;
                cm.CommentTitle = comm.CommentTitle;
                cm.CommentContent = comm.CommentContent;
                cm.IsDeleted = 0;
                cm.PostedDate =DateTime.Now;

                _blogContext.Comments.Add(cm);
                _blogContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<CommentDTO> GetComments(int id)
        {
            try
            {
                var data = (from post in _blogContext.Comments
                            where post.IsReported == id
                            select new CommentDTO()
                            {
                                Id = post.Id,
                                PostedUser = post.PostedUser,
                                CommentTitle = post.CommentTitle,
                                CommentContent = post.CommentContent,
                                IsDeleted = post.IsDeleted,
                                PostedDate = post.PostedDate,
                                PostId = post.IsReported
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<CommentDTO>();
        }
    }
}
