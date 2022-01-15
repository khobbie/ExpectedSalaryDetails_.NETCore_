using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Details.models
{
    public class SalaryDetails
    {
        public Double PAYE = 0.00; // Pay As You Earn
        public Double EPC = 0.00;  // Employee Pension Contribution
        public Double EP = 0.00;   // Employer Pension
        public Double NetSalary = 0.00; // Net Salary
        public Double GrossSalary = 0.00; // Gross Salary


        /*
            Custom Functions
         */

        // ** Employee Pension ** //
        public Double getEmployeePension(Double BasicSalaryAmount, Double EmployeePensionRate)
        {
            return EmployeePensionRate * BasicSalaryAmount;
        }

        // ** Employer Pension ** //
        public Double getEmployerPension(Double BasicSalaryAmount, Double EmployerPensionRate)
        {
            return EmployerPensionRate * BasicSalaryAmount;
        }


        // ** Tax Rate ** //
        public Double getTaxRateFromTaxableIncome(Double TaxableIncome)
        {
            Double taxRate = 0.00;

            if (TaxableIncome <= 280)
            {
                taxRate = 0.00;
            }
            else if (TaxableIncome > 280 && TaxableIncome <= 388)
            {
                taxRate = 0.05;
            }
            else if (TaxableIncome > 388 && TaxableIncome <= 528)
            {
                taxRate = 0.10;
            }
            else if (TaxableIncome > 528 && TaxableIncome <= 3528)
            {
                taxRate = 0.175;
            }
            else
            {
                taxRate = 0.25;
            }

            return taxRate;
        }


        // ** New Basic Salary after Employee Pension is deducted ** //
        public Double getNewBasicSalary(Double BasicSalaryAmount, Double TotalEmployeePensionAmount)
        {
            return BasicSalaryAmount - TotalEmployeePensionAmount;
        }


        // ** Gross Salary  ** //
        public Double getGrossSalary(Double BasicSalary, Double TotalAllowanceAmount,
                                     Double TotalEmployerPensionAmount, Double TotalTaxReliefAmount)
        {
            return BasicSalary + TotalAllowanceAmount + TotalEmployerPensionAmount + TotalTaxReliefAmount;
        }

        // ** Tax Amount  ** //
        public Double getTaxAmount(Double TaxRate, Double TaxableIncome)
        {
            return TaxRate * TaxableIncome;
        }

        // ** Net Salary  ** //
        public Double getNetSalaryAmount(Double TaxAmount, Double TaxableIncome)
        {
            return TaxableIncome - TaxAmount;
        }


    }
}
