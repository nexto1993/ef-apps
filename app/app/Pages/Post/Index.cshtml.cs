using app.Data;
using app.Pages.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace app.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly PostService _postService;

        public IndexModel(PostService postService)
        {
            _postService = postService;
        }
        [BindProperty]
        public string? RemovedPostIds { get; set; }

        [BindProperty]
        public List<PostViewModel> PostsViewModel { get; set; } = new List<PostViewModel>() { new PostViewModel() };
        public async Task<IActionResult> OnGet()
        {
            var posts = await _postService.GetPostsAsync();
            PostsViewModel = posts.Select(p => new PostViewModel { Id = p.Id, Title = p.Title }).ToList();

            if (PostsViewModel.Count == 0)
            {
                // Ensure there is at least one post for a new entry
                PostsViewModel.Add(new PostViewModel());
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
         

            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (var postViewModel in PostsViewModel)
            {
                var post = new Data.Post
                {
                    Id = postViewModel.Id,
                    Title = postViewModel.Title
                };

                if (post.Id == 0)
                {
                    // New post
                    await _postService.AddPostAsync(post);
                }
                else
                {
                    // Existing post
                    await _postService.UpdatePostAsync(post);
                }
            }

            if (!string.IsNullOrEmpty(RemovedPostIds))
            {
                var idsToRemove = RemovedPostIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(int.Parse).ToList();
                await _postService.DeletePostsAsync(idsToRemove);
            }
            return RedirectToPage("Index");
        }

    }
}
