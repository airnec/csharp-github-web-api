using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CSharpGitHubWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private const string V = "https://api.github.com/search/repositories?q=stars:>1000&sort=stars&per_page=20";
        private readonly string GitHubApiUrl = V;

        [HttpGet("Repos")]
        public async Task<IActionResult> GetRepos()
        {
            var client = new RestClient(GitHubApiUrl);
            var request = new RestRequest();
            request.AddHeader("User-Agent", "request");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }

            var json = JsonDocument.Parse(response.Content);
            var repos = json.RootElement.GetProperty("items");


            return Ok(repos);
        }
    }
}
