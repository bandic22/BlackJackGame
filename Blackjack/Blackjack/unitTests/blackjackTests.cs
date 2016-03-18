using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;
using System.Collections.Generic;

namespace unitTests
{
    [TestClass]
    public class blackjackTests
    {
        [TestMethod]
        public void TestPlayerDeal()
        {
            // Arrange
            var playerHand = new List<Card>();
            var dealerHand = new List<Card>();
            var player = new Player();
            var dealer = new Dealer();
            var deck = new Deck();
            var rnd = new Random();
            var gameloop = new GameLoop(player, dealer, playerHand, dealerHand);

            // Act
            gameloop.PlayerDeal(player, deck, rnd, playerHand);

            // Assert   
            Assert.AreEqual(player.Hand, playerHand);
        }

        public void TestDealerDeal()
        {
            // Arrange
            var playerHand = new List<Card>();
            var dealerHand = new List<Card>();
            var player = new Player();
            var dealer = new Dealer();
            var deck = new Deck();
            var rnd = new Random();
            var gameloop = new GameLoop(player, dealer, playerHand, dealerHand);

            // Act
            gameloop.DealerDeal(dealer, deck, rnd, dealerHand);

            // Assert   
            Assert.AreEqual(dealer.Hand, dealerHand);
        }
    }
}
