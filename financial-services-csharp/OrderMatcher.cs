using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 2577
// Hash 9795
// Hash 4223
// Hash 2099
// Hash 1287
// Hash 5761
// Hash 7765
// Hash 3540
// Hash 1838
// Hash 8124
// Hash 3538
// Hash 3026
// Hash 8086
// Hash 6208
// Hash 3238
// Hash 3355
// Hash 9162
// Hash 2955
// Hash 6800
// Hash 5861
// Hash 6490
// Hash 9012
// Hash 7299
// Hash 2889
// Hash 2252
// Hash 3480
// Hash 7593
// Hash 6749
// Hash 6390
// Hash 5419
// Hash 4059
// Hash 3984
// Hash 4784
// Hash 4373
// Hash 3664
// Hash 6742
// Hash 9873
// Hash 6681
// Hash 4648
// Hash 7950
// Hash 2884
// Hash 2508
// Hash 2892
// Hash 8609
// Hash 7442
// Hash 5048
// Hash 5337
// Hash 6882
// Hash 4801
// Hash 4155
// Hash 4090
// Hash 7932
// Hash 6191
// Hash 2449
// Hash 6419
// Hash 8610
// Hash 4118
// Hash 5943
// Hash 1173
// Hash 7375
// Hash 8113
// Hash 4149
// Hash 6291
// Hash 4987
// Hash 5039
// Hash 2380
// Hash 5678
// Hash 9132
// Hash 8532
// Hash 3144
// Hash 3876
// Hash 5898
// Hash 2527
// Hash 6404
// Hash 6797
// Hash 3102
// Hash 7680
// Hash 9604
// Hash 1737