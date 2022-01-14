using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Details.models.RequestModel;
using Details.models;

namespace Details.Controllers
{
    public class SalaryController : Controller
    {

        [HttpPost]
        [Route("api/salaryExpectation")]
        public JsonResult GetExpectionSalary([FromBody] Request request)
        {
            var totalAllowance = request.TotalAllowance;
            var basicSalary = request.BasicSalary;

            var SalaryDetails = new SalaryDetails();


            SalaryDetails salaryDetails = new SalaryDetails();

            Double totalEmployeePensionContribution = this.getTotalEmployeePensionContributionForTier123(basicSalary, 0.00,
                    0.055, 0.05);
            // System.out.println("totalEmployeePensionContribution: " + totalEmployeePensionContribution);

            Double totalEmployerPension = this.getTotalEmployeePensionContributionForTier123(basicSalary, 0.13,
                    0.00, 0.05);
            // System.out.println("totalEmployerPension: " + totalEmployerPension);


            Double totalGrossSalary = salaryDetails.getGrossSalary(basicSalary, totalAllowance, totalEmployerPension, 0.00);
            // System.out.println("totalGrossSalary: " + totalGrossSalary);

            Double newBasicSalary = salaryDetails.getNewBasicSalary(basicSalary, totalEmployeePensionContribution);
            // System.out.println("newBasicSalary: " + newBasicSalary);

            Double TaxableIncome = newBasicSalary + totalAllowance;
            // System.out.println("TaxableIncome: " + TaxableIncome);

            Double taxRate = salaryDetails.getTaxRateFromTaxableIncome(TaxableIncome);
            // System.out.println("taxRate: " + taxRate);

            Double taxAmount = salaryDetails.getTaxAmount(taxRate, TaxableIncome);
            // System.out.println("taxAmount: " + taxAmount);

            Double netSalary = salaryDetails.getNetSalaryAmount(taxAmount, TaxableIncome);
            // System.out.println("netSalary: " + netSalary);

            // Setting response values

            salaryDetails.PAYE = (taxAmount);
            salaryDetails.EPC = (totalEmployeePensionContribution);
            salaryDetails.EP = (totalEmployerPension);
            salaryDetails.NetSalary = (netSalary);
            salaryDetails.GrossSalary = (totalGrossSalary);


            return Json(new
            {
                PAYE = salaryDetails.PAYE,
                EPC = salaryDetails.EPC,
                EP = salaryDetails.EP,
                NetSalary = salaryDetails.NetSalary,
                GrossSalary = salaryDetails.GrossSalary

            });

        }

        protected Double getTotalEmployeePensionContributionForTier123(Double basicSalary, Double tierOnePensionRate,
                                                                Double tierTwoPensionRate, Double tierThreePensionRate)
        {
            SalaryDetails salaryDetails = new SalaryDetails();


            Double tierOnePensionAmount = salaryDetails.getEmployeePension(basicSalary, tierOnePensionRate);

            Double tierTwoPensionAmount = salaryDetails.getEmployeePension(basicSalary, tierTwoPensionRate);

            Double tierThreePensionAmount = salaryDetails.getEmployeePension(basicSalary, tierThreePensionRate);

            return tierOnePensionAmount + tierTwoPensionAmount + tierThreePensionAmount;

        }


        protected Double getTotalEmployerPensionContributionForTier123(Double basicSalary, Double tierOnePensionRate,
                                                                       Double tierTwoPensionRate, Double tierThreePensionRate)
        {
            SalaryDetails salaryDetails = new SalaryDetails();


            Double tierOnePensionAmount = salaryDetails.getEmployerPension(basicSalary, tierOnePensionRate);

            Double tierTwoPensionAmount = salaryDetails.getEmployerPension(basicSalary, tierTwoPensionRate);

            Double tierThreePensionAmount = salaryDetails.getEmployerPension(basicSalary, tierThreePensionRate);

            return tierOnePensionAmount + tierTwoPensionAmount + tierThreePensionAmount;

        }

    }
}
