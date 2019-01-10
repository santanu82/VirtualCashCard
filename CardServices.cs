using System;
using System.Collections.Generic;
using System.Linq;

namespace VirtualCashCard.ServiceProviders
{
    public class CardServices : ICardServices
    {
        /// <summary>
        /// This method returns balance of the card provided by the customer
        /// </summary>
        /// <param name="cardPin">pin number for the card</param>
        /// <returns>returns balance</returns>
        public double GetCardBalance(int cardPin)
        {
            var balance = 0.0;
            try
            {
                var cardDetail = GetCardDetailsByPin(cardPin);
                if (cardDetail != null)
                {
                    balance = cardDetail.Balance;
                }
                else
                {
                    throw new Exception("Pin number provided do not matches to our records :" + cardPin);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Please provide numeric pin number :" + ex.Message);
            }
           
            return balance;
        }
        /// <summary>
        /// This method top up the cash amount to the existing card for the customer
        /// </summary>
        /// <param name="cardPin">pin number for the card</param>
        /// <param name="topupAmount">cash amount to be top up</param>
        /// <returns>new balance for the existing card</returns>
        public double TopUpCard(int cardPin, double topupAmount)
        {
            var newBalance = 0.0;
            try
            {
                if (ValidateCard(cardPin))
                {
                    var currentBalance = GetCardBalance(cardPin);
                    newBalance = currentBalance + topupAmount;
                    var cardDetails = UpdateAllCardDetails(cardPin, newBalance);
                    var cardDetail = (from c in cardDetails
                                      where c.CardPin == cardPin
                                      select c).FirstOrDefault();
                    return cardDetail.Balance;
                }
                else
                {
                    throw new Exception("Pin number provided do not matches to our records :" + cardPin);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Please provide numeric pin number :" + ex.Message);
            }
            
            
        
        }
        /// <summary>
        /// This method validate the card provided by the customer
        /// </summary>
        /// <param name="cardPin">pin number for the card</param>
        /// <returns>returns true if it is valid card or false</returns>
        public bool ValidateCard(int cardPin)
        {
            bool isValid = true;
            try
            {
                if (GetCardDetailsByPin(cardPin) != null)
                {
                    isValid = true;
                }

                else
                {
                    isValid = false;
                    throw new Exception("Pin number provided do not matches to our records :" + cardPin);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Please provide numeric pin number :" + ex.Message);
            }
            
            return isValid;
        }
        /// <summary>
        /// This method returns the card details based on the valid pin number
        /// </summary>
        /// <param name="cardPin">pin number for the card</param>
        /// <returns>returns the card details</returns>
        public CardDetails GetCardDetailsByPin(int cardPin)
        {
            try
            {
                var cardList = GetAllCardDetails();
                var cardDetail = (from c in cardList where c.CardPin == cardPin select c).FirstOrDefault();
                return cardDetail;
            }
            catch (Exception ex)
            {

                throw new Exception("Please provide numeric pin number :" + ex.Message);
            }
            
        }

        /// <summary>
        /// Get all card details
        /// </summary>
        /// <returns></returns>
        public List<CardDetails> GetAllCardDetails()
        {
            var cardList = new List<CardDetails>
            {
                new CardDetails
                {
                    CardNumber = 6432987908032324,
                    CardPin = 3123,
                    Balance = 4325.78
                },
                new CardDetails
                {
                    CardNumber = 3244987908032324,
                    CardPin = 9732,
                    Balance = 324
                },
                new CardDetails
                {
                    CardNumber = 5398749098320948,
                    CardPin = 5634,
                    Balance = 532.98
                },
                new CardDetails
                {
                    CardNumber = 4789432998093232,
                    CardPin = 8943,
                    Balance = 7988.67
                }
            };
            return cardList;

        }
        /// <summary>
        /// This method allows the customer to draw the cash from their card
        /// </summary>
        /// <param name="cardPin">pin number for the card</param>
        /// <param name="amountToBeDrawn">cash amount the customer wants to draw</param>
        /// <returns></returns>
        public double DrawCash(int cardPin, double amountToBeDrawn)
        {
            var existingBalance = 0.0;
            try
            {
                if (ValidateCard(cardPin))
                {
                    var currentBalance = GetCardBalance(cardPin);
                    if (currentBalance >= amountToBeDrawn)
                    {
                        existingBalance = currentBalance - amountToBeDrawn;
                        var cardDetails = UpdateAllCardDetails(cardPin, existingBalance);
                        var cardDetail = (from c in cardDetails
                                          where c.CardPin == cardPin
                                          select c).FirstOrDefault();
                        return cardDetail.Balance;
                    }
                    else
                    {
                        throw new Exception("You do not have sufficient funds available.");
                    }
                }
                else
                {
                    throw new Exception("Pin number provided do not matches to our records :" + cardPin);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Please provide numeric pin number :" + ex.Message);
            }


        }
        /// <summary>
        /// This method provides the existing balance for both cash top up and cash drawing
        /// </summary>
        /// <param name="cardPin">pin number for card</param>
        /// <param name="cashAmount">cash amount</param>
        /// <returns></returns>
        private CardDetails UpdateCardDetails(int cardPin,double cashAmount)
        {
            var cardList = GetAllCardDetails();
            var existingCard = (from c in cardList where c.CardPin == cardPin select c).FirstOrDefault();
            existingCard.Balance = cashAmount;
            return existingCard;
        }
        /// <summary>
        /// This method update all card details which has been top up or drawn
        /// </summary>
        /// <param name="cardPin"></param>
        /// <param name="cashAmount"></param>
        /// <returns></returns>
        private List<CardDetails> UpdateAllCardDetails(int cardPin,double cashAmount)
        {
            var cardList = GetAllCardDetails();
            var existingCard = (from c in cardList where c.CardPin == cardPin select c).FirstOrDefault();
            cardList.Remove(existingCard);
            var updatedCard = UpdateCardDetails(cardPin, cashAmount);
            cardList.Add(updatedCard);
            return cardList;
        }

       
    }
}
