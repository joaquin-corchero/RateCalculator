using System;

namespace RateCalculator.Models
{
    public class Quote
    {
        public double RequestedAmmount { get; set; }
        public double Rate { get; set; }
        public double MonthlyRepayment { get; set; }
        public double TotalRepayment { get; set; }

        Quote(double requestedAmmount, double interestRate, double totalAmmountToPay, double monthlyRepayment)
        {
            RequestedAmmount = requestedAmmount;
            Rate = interestRate;
            TotalRepayment = totalAmmountToPay;
            MonthlyRepayment = monthlyRepayment;
        }

        public static Quote Create(double loanAmmount, double interestRate, double totalAmmountToPay, double monthlyRepayment)
        {
            return new Quote(loanAmmount, interestRate, totalAmmountToPay, monthlyRepayment);
        }
    }
}