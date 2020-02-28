using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Order Book
    /// https://codeforces.com/gym/269631/problem/A
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 1.2")]
    public class OrderBook
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(Order[] orders, int orderBookDepth, Order[] expectedOrderBook)
        {
            var result = CountOrderBook(orders, orderBookDepth).ToArray();
            Assert.Equal(expectedOrderBook.Select(o => o.ToString()), result.Select(o => o.ToString()));
        }

        public static IEnumerable<object[]> InputData => new TheoryData<Order[], int, Order[]>
        {
            {
                new[]
                {
                    Order.NewBuyOrder(10, 3),
                    Order.NewSellOrder(50, 2),
                    Order.NewSellOrder(40, 1),
                    Order.NewSellOrder(50, 6),
                    Order.NewBuyOrder(20, 4),
                    Order.NewBuyOrder(25, 10),
                },
                2,
                new[]
                {
                    Order.NewSellOrder(50, 8),
                    Order.NewSellOrder(40, 1),
                    Order.NewBuyOrder(25, 10),
                    Order.NewBuyOrder(20, 4)
                }
            },
        };

        private static IEnumerable<Order> CountOrderBook(Order[] orders, int orderBookDepth)
        {
            var sellOrders = new SortedDictionary<int, int>();
            var buyOrders = new SortedDictionary<int, int>();

            foreach (var order in orders)
            {
                if (order.Direction == Order.DirectionType.Buy)
                {
                    if (buyOrders.ContainsKey(order.Price))
                    {
                        buyOrders[order.Price] += order.Quantity;
                    }
                    else
                    {
                        buyOrders.Add(order.Price, order.Quantity);
                    }
                }

                if (order.Direction == Order.DirectionType.Sell)
                {
                    if (sellOrders.ContainsKey(order.Price))
                    {
                        sellOrders[order.Price] += order.Quantity;
                    }
                    else
                    {
                        sellOrders.Add(order.Price, order.Quantity);
                    }
                }
            }

            var resultOrders = new List<Order>();
            resultOrders.AddRange(sellOrders.Take(orderBookDepth).Reverse().Select(o => Order.NewSellOrder(o.Key, o.Value)));
            resultOrders.AddRange(buyOrders.Reverse().Take(orderBookDepth).Select(o => Order.NewBuyOrder(o.Key, o.Value)));
            return resultOrders.ToArray();
        }

        public class Order : IEquatable<Order>
        {
            private const string Buy = "B";
            private const string Sell = "S";

            public Order(string direction, int price, int quantity)
            {
                Direction = direction == Buy
                    ? DirectionType.Buy
                    : direction == Sell
                        ? Direction = DirectionType.Sell
                        : throw new ArgumentException(nameof(direction));
                Price = price;
                Quantity = quantity;
            }

            public DirectionType Direction { get; }
            public int Price { get; }
            public int Quantity { get; }

            public override string ToString()
            {
                return string.Format("{0} {1} {2}",  Direction == DirectionType.Buy ? Buy : Sell, Price.ToString(), Quantity.ToString());
            }

            public static Order NewBuyOrder(int price, int quantity)
            {
                return new Order("B", price, quantity);
            }

            public static Order NewSellOrder(int price, int quantity)
            {
                return new Order("S", price, quantity);
            }

            public enum DirectionType
            {
                Sell,
                Buy
            }

            public bool Equals(Order other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Direction == other.Direction && Price == other.Price && Quantity == other.Quantity;
            }
        }


        private static void Main1()
        {
            var n_s = Console.ReadLine().Split(new[] { ' ' }).Select(str => int.Parse(str)).ToArray();
            //var sequence = new int[n];

            const string buy = "B";
            const string sell = "S";
            int n = n_s[0];
            int s = n_s[1];

            var orders = new List<Order>(n_s[0]);

            for (int i = 0; i < n; i++)
            {
                var d_p_q = Console.ReadLine().Split(new[] { ' ' });

                string d = d_p_q[0];
                int p = int.Parse(d_p_q[1]);
                int q = int.Parse(d_p_q[2]);

                orders.Add(new Order(d, p, q));
            }

            var result = CountOrderBook(orders.ToArray(), s);

            foreach (var order in result)
            {
                Console.WriteLine(order);
            }
        }
    }
}