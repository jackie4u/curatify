using Microsoft.AspNetCore.Mvc;
using FeedDataLibrary.Services;
using DimePubWeb.Models.ViewModels;

//todo: implement error handling
namespace DimePubWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedRepository feedRepository;

        public int PageSize = 10;

        public HomeController(IFeedRepository repo)
        {
            this.feedRepository = repo;
            // TODO: Re-immplement refresh on start-up
            repo.RefreshAllFeeds();
        }

        public ActionResult Index(DateTime? startDate, DateTime? endDate, int episodePage = 1)
        {
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.Now;

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;

            return View(new Episodes
            {
                EpisodesList = feedRepository.FeedItems
                        .Where(e => e.DatePublished >= startDate && e.DatePublished <= endDate)
                        .OrderByDescending(f => f.DatePublished)
                        .Skip((episodePage - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = episodePage,
                    ItemsPerPage = PageSize,
                    TotalItems = feedRepository.FeedItems.Count()
                }
            });
        }

        [HttpPost]
        public ActionResult PodcastRefreshAll()
        {
            int episodePage = 1;
            feedRepository.RefreshAllFeeds();
            return View("Index", new Episodes
            {
                EpisodesList = feedRepository.FeedItems
                        .OrderByDescending(f => f.DatePublished)
                        .Skip((episodePage - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = episodePage,
                    ItemsPerPage = PageSize,
                    TotalItems = feedRepository.FeedItems.Count()
                }
            });
        }

        [HttpPost]
        public ActionResult PodcastRefresh(int id) // id = feedID
        {
            feedRepository.RefreshFeed(id);
            return View("PodcastDetail", new PodcastDetail(feedRepository.GetFeedById(id)));
        }

        public ViewResult Podcasts(string searchString)
        {
            return View(new Podcasts(feedRepository.Feeds, searchString));
        }

        public ActionResult PodcastDetail(int id) // id = feedID
        {
            return View(new PodcastDetail(feedRepository.GetFeedById(id)));
        }

        [HttpPost]
        public ActionResult PodcastDelete(int id) // id = feedID
        {
            feedRepository.DeleteFeed(id);
            return View("PodcastDeleted");
        }

        [HttpGet]
        public ViewResult PodcastAdd()
        {
            return View();
        }

        [HttpPost]
        public ViewResult PodcastAdd(PodcastAdd podcastAdd)
        {
            if ((ModelState.IsValid) && (podcastAdd.SourceUri != null))
            {
                int feedId = feedRepository.AddFeed(podcastAdd.SourceUri, podcastAdd.Note);
                if (feedId == 0)
                {
                    // TODO: Temporary error hadnling - validation error
                    return View("PodcastAddError", podcastAdd);
                }
                else
                    return View("PodcastDetail", new PodcastDetail(feedRepository.GetFeedById(feedId)));
            }
            else
            {
                return View();
            }
        }
    }
}
