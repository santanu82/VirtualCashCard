using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCashCard.ServiceProviders
{
    public interface ICardServices
    {
        double GetCardBalance(int cardPin);
        double TopUpCard(int cardPin, double topupAmount);
        double DrawCash(int cardPin, double amountToBeDrawn);
        bool ValidateCard(int cardPin);
        List<CardDetails> GetAllCardDetails();
        CardDetails GetCardDetailsByPin(int cardPin);
      
    }
}
