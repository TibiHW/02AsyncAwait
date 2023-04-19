using CloudServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices.ObjectiveC;
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

            var visitCountTask = await staticRegTask.ContinueWith(
                async (antecedent, state) => await _statisticService.GetVisitsCountAsync(state.ToString()),
                path);

            var result = await visitCountTask;

            return View(null, result);
        }
    }
}
