using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace ProfitCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Value is required.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Invalid value.")]
        public double InitialAmount { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Value is required.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Invalid value.")]
        public double PercentPerMonth { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Value is required.")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Invalid value.")]
        public double HowManyYears { get; set; }

        public readonly double TAX = 0.25;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }


        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                double ans = InitialAmount + PercentPerMonth + HowManyYears;
                Message = "Your Profit After " + HowManyYears + " Years Will Be : " + Calculate().ToString("0.00") + "🤑";
            }
        }

        public double Calculate()
        {
            double profit = (InitialAmount * PercentPerMonth) * (1 - TAX);
            double newAmount = InitialAmount + profit;
            int numberOfMoonth = (int)(HowManyYears * 12);

            for (int i = 1; i < numberOfMoonth; i++)
            {
                profit = (newAmount * PercentPerMonth) * (1 - TAX);
                newAmount = newAmount + profit;
            }

            return newAmount;
        }
    }
}