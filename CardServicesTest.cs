using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualCashCard.ServiceProviders;
namespace VirtualCashCard.Tests
{
    [TestClass]
    public class CardServicesTest
    {
        CardServices cardServices = new CardServices();
        [TestMethod]
        public void ValidatingCorrectCard()
        {
            
            //arrange
            var pinProvided = 3123;
            
            //act
            var expectedStatus = true;
            var actualStatus = cardServices.ValidateCard(pinProvided);
            //assert
            Assert.AreEqual(actualStatus, expectedStatus);
        }
        [TestMethod]
        public void Is_Available_Balance_Returns_Correctly_After_Cash_Top_Up()
        {
            //arrange
            var pinProvided = 3123;
            var topUpAmount = 100.0;
            //act
            var expectedBalanceAfterTopup = 4425.78;
            var actualBalanceAfterTopup = cardServices.TopUpCard(pinProvided, topUpAmount);
            //assert
            Assert.AreEqual(expectedBalanceAfterTopup, actualBalanceAfterTopup);
        }
        [TestMethod]
        public void Is_Available_Balance_Returns_Correctly_After_Cash_Draw()
        {
            //arrange
            var pinProvided = 3123;
            var drawAmount = 100.0;
            //act
            var expectedBalanceAfterDraw = 4225.78;
            var actualBalanceAfterDraw = cardServices.DrawCash(pinProvided, drawAmount);
            //assert
            Assert.AreEqual(expectedBalanceAfterDraw, actualBalanceAfterDraw);
        }
    }
   
}
