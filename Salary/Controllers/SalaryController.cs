using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Salary.models.RequestModel.cs;
using Salary.models;

namespace Salary.Controllers
{
    public class SalaryController : Controller
    {
       //Lookup at something here ..

        [HttpPost]
        [Route("api/salaryExpectation")]
        public JsonResult Create([FromBody] Request request)
        {
            var allowance = request.TotalAllowance;
            var salary = request.NetSalary;




            // TEIR 1
            var tier1 = new Teir();
            tier1.EmployeePensionRate = (0.00);
            tier1.EmployerPensionRate = (0.13);
            tier1.Allowance = (allowance);
            Double basicSalary_1 = tier1.getBasicSalaryAmount(salary, allowance);
            tier1.getGrossSalaryAmount(salary, allowance);
            tier1.getEmployeePensionContribution(basicSalary_1);
            tier1.getEmployerPensionContribution(basicSalary_1);
            tier1.getTotalPAYETax(basicSalary_1, allowance);

            // TEIR 2
            var tier2 = new Teir();
            tier2.EmployeePensionRate = (0.055);
            tier2.EmployerPensionRate = (0.000);
            tier2.Allowance = (allowance);
            Double basicSalary_2 = tier2.getBasicSalaryAmount(salary, allowance);
            tier2.getGrossSalaryAmount(salary, allowance);
            tier2.getEmployeePensionContribution(basicSalary_2);
            tier2.getEmployerPensionContribution(basicSalary_2);
            tier2.getTotalPAYETax(basicSalary_2, allowance);


            // TEIR 3
            var tier3 = new Teir();
            tier3.EmployeePensionRate = (0.05);
            tier3.EmployerPensionRate = (0.05);
            tier3.Allowance = (allowance);
            Double basicSalary_3 = tier3.getBasicSalaryAmount(salary, allowance);
            tier3.getGrossSalaryAmount(salary, allowance);
            tier3.getEmployeePensionContribution(basicSalary_3);
            tier3.getEmployerPensionContribution(basicSalary_3);
            tier3.getTotalPAYETax(basicSalary_3, allowance);



            return Json(new { 
                tier1 = new { GrossSalary = tier1.GrossSalary, BasicSalary = tier1.BasicSalary, EmployeePensionRate = tier1.EmployeePensionRate, EmployeePensionAmount = tier1.EmployeePensionAmount, EmployerPensionRate = tier1.EmployerPensionRate, EmployerPensionAmount = tier1.EmployerPensionAmount, TaxRate = tier1.TaxRate, TotalPAYETax = tier1.TotalPAYETax } 
                ,
                tier2 = new { GrossSalary = tier2.GrossSalary, BasicSalary = tier2.BasicSalary, EmployeePensionRate = tier2.EmployeePensionRate, EmployeePensionAmount = tier2.EmployeePensionAmount, EmployerPensionRate = tier2.EmployerPensionRate, EmployerPensionAmount = tier2.EmployerPensionAmount, TaxRate = tier2.TaxRate, TotalPAYETax = tier2.TotalPAYETax }
                ,
                tier3 = new { GrossSalary = tier3.GrossSalary, BasicSalary = tier3.BasicSalary, EmployeePensionRate = tier3.EmployeePensionRate, EmployeePensionAmount = tier3.EmployeePensionAmount, EmployerPensionRate = tier3.EmployerPensionRate, EmployerPensionAmount = tier3.EmployerPensionAmount, TaxRate = tier3.TaxRate, TotalPAYETax = tier3.TotalPAYETax }

            });
        }

    }
}
