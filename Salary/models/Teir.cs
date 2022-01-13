using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Salary.models
{
    public class Teir
    {
        public Double GrossSalary = 0.00; 
        public Double BasicSalary = 0.00;
        public Double Allowance = 0.00;
       
        public Double EmployeePensionAmount = 0.00;
        public Double EmployeePensionRate = 0.00;
        
        public Double EmployerPensionAmount = 0.00;
        public Double EmployerPensionRate = 0.13;

        public Double TaxRate = 0.00;
        public Double TotalPAYETax = 0.00;


        /*
        CUSTOM FUNCTIONS
         */


        public Double getTaxRateFromNetSalary(Double NetSalary)
        {
            Double taxRate = 0.00;

            if (NetSalary <= 280)
            {
                taxRate = 0.00;
            }
            else if (NetSalary > 280 && NetSalary <= 388)
            {
                taxRate = 0.5;
            }
            else if (NetSalary > 388 && NetSalary <= 528)
            {
                taxRate = 0.10;
            }
            else if (NetSalary > 528 && NetSalary <= 3528)
            {
                taxRate = 0.175;
            }
            else
            {
                taxRate = 0.25;
            }
            this.TaxRate = taxRate;
            return taxRate;
        }



        public Double getBasicSalaryAmount(Double NetSalary, Double TotalAllowance)
        {
            Double taxRate = getTaxRateFromNetSalary(NetSalary);

            Double basicSalary =
                    ((NetSalary - ((taxRate * TotalAllowance) + TotalAllowance)) / (1 + EmployeePensionRate - taxRate));

            this.BasicSalary = basicSalary;
            return basicSalary;
        }

        public Double getGrossSalaryAmount(Double BasicSalaryAmount, Double TotalAllowance)
        {
            Double grossSalaryAmount = BasicSalaryAmount + TotalAllowance;

            this.GrossSalary = (grossSalaryAmount);

            return grossSalaryAmount;
        }

        public Double getEmployeePensionContribution(Double BasicSalaryAmount)
        {
            Double EmployeePensionContributionAmount = (EmployeePensionRate * BasicSalaryAmount);

            this.EmployeePensionAmount = (EmployeePensionContributionAmount);

            return EmployeePensionContributionAmount;
        }

        public Double getEmployerPensionContribution(Double BasicSalaryAmount)
        {
            Double EmployerPensionContributionAmount = (EmployerPensionRate * BasicSalaryAmount);

            this.EmployerPensionAmount = (EmployerPensionContributionAmount);

            return EmployerPensionContributionAmount;
        }

        public Double getTotalPAYETax(Double BasicSalaryAmount, Double TotalAllowance)
        {
            Double grossSalaryAmount = getGrossSalaryAmount(BasicSalaryAmount, TotalAllowance);

            Double EmployerPensionContributionAmount = getEmployeePensionContribution(BasicSalaryAmount);

            Double TaxableIncomeAmount = grossSalaryAmount - EmployerPensionContributionAmount;

            Double TaxableRate = getTaxRateFromNetSalary(BasicSalaryAmount + TotalAllowance);

            Double TotalPAYETax = (TaxableRate * TaxableIncomeAmount);

            this.TotalPAYETax = (TotalPAYETax);

            return TotalPAYETax;
        }

        /*
         END CUSTOM FUNCTIONS
       */

       
    }


  

}
