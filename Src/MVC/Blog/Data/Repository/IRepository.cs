using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.Entities;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        Task<Post> GetPostAsync(int? id);
        
        Task<IEnumerable<Post>> GetAllPostsAsync();
        
        void AddPost(Post post);
        
        void UpdatePost(Post post);
        
        void RemovePost(int id);

        Task<bool> PostExistsAsync(int id);

        Task<bool> SaveChangesAsync();

    }
}
