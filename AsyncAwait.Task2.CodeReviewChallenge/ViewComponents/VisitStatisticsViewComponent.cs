using CloudServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.ViewComponents
{
    public class VisitStatisticsViewComponent : ViewComponent
    {
        private readonly IStatisticService _statisticService;

        public VisitStatisticsViewComponent(IStatisticService statisticService)
        {
            _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
        }

        public async Task<IViewComponentResult> InvokeAsync(string path)
        {
            var staticRegTask = Task.Run(() => _statisticService.RegisterVisitAsync(path));

            var result = await _statisticService.GetVisitsCountAsync(path);
            result += 1;

            return View(null, result);
        }
    }
}
