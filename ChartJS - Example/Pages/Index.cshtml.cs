using ChartExample.Models;
using ChartExample.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChartJS___Example.Pages
{
    public class IndexModel : PageModel
    {
        public ChartJs BarChart { get; set; }
        public string ChartJson { get; set; }
        public DB db { get; set; }
        public IndexModel(DB db)
        {
            BarChart = new ChartJs();   // must initialise the ChartJs object in the constructor
            this.db = db;
        }

        public void OnGet()
        {
            Dictionary<string, int> favCodeEditors = db.getFavouriteCodeEditors();

            setUpBarChart(favCodeEditors);
        }

        private void setUpBarChart(Dictionary<string, int> dataToDisplay)
        {
            try
            {
                // 1. set up chart options
                BarChart.type = "bar";
                BarChart.options.responsive = true;

                // 2. separate the received Dictionary data into labels and data arrays
                var labelsArray = new List<string>();
                var dataArray = new List<double>();

                foreach (var data in dataToDisplay)
                {
                    labelsArray.Add(data.Key);
                    dataArray.Add(data.Value);
                }

                BarChart.data.labels = labelsArray;

                // 3. set up a dataset
                var firsDataset = new Dataset();
                firsDataset.label = "Number of votes";
                firsDataset.data = dataArray.ToArray();

                BarChart.data.datasets.Add(firsDataset);

                // 4. finally, convert the object to json to be able to inject in HTML code
                ChartJson = JsonConvert.SerializeObject(BarChart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error initialising the bar chart inside Index.cshtml.cs");
                throw e;
            }
        }
    }
}
