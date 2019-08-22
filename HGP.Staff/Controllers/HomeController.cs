using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HGP.Staff.Models;
using HGP.Staff.Services;
using HGP.Staff.DomainModel;

namespace HGP.Staff.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDepartmentService _service;
        public HomeController(IDepartmentService service) => _service = service;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new AddDepartmentsModel { Time = DateTime.Now.Date };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentsModel model)
        {
            model.Time = model.Time.Date;
            var models = new List<Department>
            {
                new Department { Organization = Organization.CEO, Time = model.Time, EmployeeNumber = model.CEONo },
                new Department { Organization = Organization.InternalControl, Time = model.Time, EmployeeNumber = model.InternalControlNo },
                new Department { Organization = Organization.StrategicInvestment, Time = model.Time, EmployeeNumber = model.StrategicInvestmentNo },
                new Department { Organization = Organization.HumanResources, Time = model.Time, EmployeeNumber = model.HumanResourcesNo },
                new Department { Organization = Organization.FinancialManagement, Time = model.Time, EmployeeNumber = model.FinancialManagementNo },
                new Department { Organization = Organization.ProductDevelopment, Time = model.Time, EmployeeNumber = model.ProductDevelopmentNo },
                new Department { Organization = Organization.SupplyChain, Time = model.Time, EmployeeNumber = model.SupplyChainNo },
                new Department { Organization = Organization.UserCenter, Time = model.Time, EmployeeNumber = model.UserCenterNo },
                new Department { Organization = Organization.ExternalContact, Time = model.Time, EmployeeNumber = model.ExternalContactrNo }
            };
            await _service.AddRangeAsync(models);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _service.GetllAllAsync();
            var data = list.GroupBy(n => n.Organization);
            var charts = new ChartData { Title = "人员变化图" };
            foreach (var item in data)
            {
                var org = item.Key.EnumDesc();
                charts.Legend.Add(org);
                var serieData = new List<int>();
                var xAxis = new List<string>();
                foreach (var depart in item.OrderBy(n => n.Time))
                {
                    xAxis.Add(depart.Time.ToString("MM/dd"));
                    serieData.Add(depart.EmployeeNumber);
                }
                if (!charts.XAxisData.Any())
                    charts.XAxisData.AddRange(xAxis);
                charts.Series.Add(org, serieData);
            }

            var totalStr = "总数";
            charts.Legend.Add(totalStr);
            var totalData = list.OrderBy(n => n.Time).GroupBy(n => n.Time);
            var totalSerie = totalData.Select(item => item.Sum(n => n.EmployeeNumber)).ToList();
            charts.Series.Add(totalStr, totalSerie);
            return Json(charts);
        }

        public async Task<IActionResult> Table()
        {
            var list = await _service.GetllAllAsync();
            var data = list.GroupBy(n => n.Time);
            var model = new TableModel();
            
            foreach (var item in data)
            {
                var rows = new List<string>();
                var i = item.OrderBy(n => n.Organization);
                rows.Add(item.Key.ToString("yyyy-MM-dd"));
                rows.AddRange(i.Select(oo => oo.EmployeeNumber.ToString()));
                rows.Add(i.Select(n=>n.EmployeeNumber).Sum().ToString());
                model.RowsData.Add(rows);

                if (!model.Org.Any())
                {
                    var orgs = item.Select(n => n.Organization).OrderBy(n => n);
                    var os = new List<string> { "" };
                    os.AddRange(orgs.Select(o => o.EnumDesc()));
                    os.Add("总数");
                    model.Org = os;
                }
                
            }
            return View(model);
        }

    }
}
