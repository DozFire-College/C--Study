using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        private List<string> playerCards = new List<string>();
        private List<string> dealerCards = new List<string>();
        private List<string> deck = new List<string>();
        private Random random = new Random();

        private int wins = 0;
        private int losses = 0;
        private int draws = 0;
        private bool gameOver = false;

        private string[] suits = { "♠", "♥", "♦", "♣" };
        private string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameOver)
            {
                AddCardToPlayer();
            }
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            if (!gameOver)
            {
                CheckResult();
            }
        }

        private void StartNewGame()
        {
            playerCards.Clear();
            dealerCards.Clear();

            CreateDeck();
            ShuffleDeck();

            playerCards.Add(DrawCard());
            dealerCards.Add(DrawCard());
            playerCards.Add(DrawCard());
            dealerCards.Add(DrawCard());

            UpdateUI();

            gameOver = false;
            StartButton.Content = "Новая игра";
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            ResultText.Text = "";

            if (CalculateScore(playerCards) == 21)
            {
                ResultText.Text = "БЛЭКДЖЕК! Вы выиграли!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Gold);
                wins++;
                EndGame();
            }
        }

        private void AddCardToPlayer()
        {
            playerCards.Add(DrawCard());
            UpdateUI();

            int playerScore = CalculateScore(playerCards);
            if (playerScore > 21)
            {
                ResultText.Text = $"ПЕРЕБОР! У вас {playerScore} очков. Вы проиграли!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Red);
                losses++;
                EndGame();
            }
            else if (playerScore == 21)
            {
                ResultText.Text = "У вас 21! Проверьте результат.";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Yellow);
                CheckResult();
            }
        }

        private void CheckResult()
        {
            while (CalculateScore(dealerCards) < 17)
            {
                dealerCards.Add(DrawCard());
            }

            UpdateUI();

            int playerScore = CalculateScore(playerCards);
            int dealerScore = CalculateScore(dealerCards);

            if (dealerScore > 21)
            {
                ResultText.Text = $"Дилер перебрал ({dealerScore} очков). Вы выиграли!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Gold);
                wins++;
            }
            else if (playerScore > dealerScore)
            {
                ResultText.Text = $"У вас {playerScore}, у дилера {dealerScore}. Вы выиграли!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Gold);
                wins++;
            }
            else if (playerScore < dealerScore)
            {
                ResultText.Text = $"У вас {playerScore}, у дилера {dealerScore}. Вы проиграли!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Red);
                losses++;
            }
            else
            {
                ResultText.Text = $"Ничья! У обоих по {playerScore} очков.";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Colors.Yellow);
                draws++;
            }

            EndGame();
        }

        private void EndGame()
        {
            gameOver = true;
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
            StartButton.Content = "Новая игра";
  
        }

        private void CreateDeck()
        {
            deck.Clear();
            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    deck.Add($"{value}{suit}");
                }
            }
        }

        private void ShuffleDeck()
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        private string DrawCard()
        {
            if (deck.Count == 0)
            {
                CreateDeck();
                ShuffleDeck();
            }

            string card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        private int CalculateScore(List<string> cards)
        {
            int score = 0;
            int aces = 0;

            foreach (string card in cards)
            {
                string value = card.Substring(0, card.Length - 1);

                if (value == "A")
                {
                    aces++;
                    score += 11;
                }
                else if (value == "K" || value == "Q" || value == "J")
                {
                    score += 10;
                }
                else
                {
                    score += int.Parse(value);
                }
            }

            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }

            return score;
        }

        private void UpdateUI()
        {
            string playerCardsDisplay = "Ваши карты: " + string.Join(" ", playerCards);
            PlayerCardsText.Text = playerCardsDisplay;
            PlayerScoreText.Text = $"Очки: {CalculateScore(playerCards)}";

            string dealerCardsDisplay;
            if (gameOver)
            {
                dealerCardsDisplay = "Карты дилера: " + string.Join(" ", dealerCards);
                DealerScoreText.Text = $"Очки: {CalculateScore(dealerCards)}";
            }
            else
            {
                dealerCardsDisplay = "Карты дилера: " + dealerCards[0] + " [?]";
                DealerScoreText.Text = "Очки: ?";
            }
            DealerCardsText.Text = dealerCardsDisplay;
        }
    }
}