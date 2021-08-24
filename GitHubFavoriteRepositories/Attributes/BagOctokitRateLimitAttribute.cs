using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Octokit;

namespace GitHubFavoriteRepositories.Attributes
{
    public class BagOctokitRateLimitAttribute : ActionFilterAttribute
    {
        private readonly GitHubClient _client;

        public BagOctokitRateLimitAttribute(GitHubClient client)
            => _client = client;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller controller)
            {
                var resources = _client.Miscellaneous.GetRateLimits().Result.Resources;
                controller.ViewBag.RateLimit = resources.Core;
            }
        }
    }
}
