using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class PostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var existingPost = _context.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                // Update other fields as necessary
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePostsAsync(List<int> postIds)
        {
            var postsToRemove = _context.Posts.Where(p => postIds.Contains(p.Id)).ToList();
            _context.Posts.RemoveRange(postsToRemove);
            await _context.SaveChangesAsync();
        }


    }
}
