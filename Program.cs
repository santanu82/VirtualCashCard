using System;
using VirtualCashCard.ServiceProviders;

namespace VirtualCashCard
{
    public class Program
    {
        private static readonly CardServices _cardServices = new CardServices();

        
        public static void Main(string[] args)
        {
            Console.WriteLine("==================Welome to Virtual Cash Card Services=========================");
            Console.Beep();
            Console.WriteLine("Please enter the pin");
            var pin = Convert.ToInt32(Console.ReadLine());
            var balance = _cardServices.GetCardBalance(pin);
            Console.WriteLine("The balance of your card is :" + balance);
            Console.Beep();
            Console.WriteLine("==========================================================");
            Console.WriteLine("Please enter the amount you want to draw");
            var drawingAmount = Convert.ToDouble(Console.ReadLine());
            var availableBalance = _cardServices.DrawCash(pin, drawingAmount);
            Console.WriteLine("Your available balance is :" + availableBalance);
            Console.Beep();
            Console.WriteLine("==========================================================");
            Console.WriteLine("Please enter the top up amount");
            var topupAmount = Convert.ToDouble(Console.ReadLine());
            var newBalance = _cardServices.TopUpCard(pin, topupAmount);
            Console.WriteLine("Your new balance is :" + newBalance);
            Console.ReadLine();



        }
    }
}
