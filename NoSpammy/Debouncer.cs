using Dalamud.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NoSpammy
{
    public class Debouncer
    {
        private readonly Dictionary<string, CancellationTokenSource> cancellationTokens = new Dictionary<string, CancellationTokenSource>();

        public bool Debounce(string key, int milliseconds)
        {
            var result = true;
            if (cancellationTokens.TryGetValue(key, out var value))
            {
                PluginLog.LogDebug($"Debounce prevented action: \"{key}\"");
                value.Cancel();
                cancellationTokens.Remove(key);
                result = false;
            }
            else
            {

                PluginLog.LogDebug($"Debounce allowed action: \"{key}\"");
            }

            var cts = new CancellationTokenSource();
            cancellationTokens.Add(key, cts);
            Task.Delay(milliseconds).ContinueWith(t => {
                cancellationTokens.Remove(key);
                PluginLog.LogDebug($"Debounce completed for action: \"{key}\"");
            }, cts.Token);

            return result;
        }
    }
}
