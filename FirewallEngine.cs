using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MicroSegEnforcer
{
  public static class FirewallEngine
    {
        public static void EvaluateTraffic(FirewallPolicy policy, string srcIp, string dstIp, int port)
        {
            Console.WriteLine($"Evaluating packet: {srcIp} -> {dstIp}:{port}");

            string decision = "Deny Implicit Drop";
            foreach (var rule in policy.Rules)
            {
                bool srcMatch = EvaluateIpMatch(rule.SourceIp, srcIp);
                bool dstMatch = EvaluateIpMatch(rule.DestinationIp, dstIp);
                bool portMatch = rule.Port == 0 || rule.Port == port;

                if (srcMatch && dstMatch && portMatch)
                {
                    decision = $"{rule.Action} (Matched: {rule.RuleName})";
                    break;
                }
            }

            Console.WriteLine($"Verdict: {decision}\n");
        }

        public static bool EvaluateIpMatch(string ruleIP, string targetIpStr)
        {

            if (ruleIP == "*") return true;
            if (string.Equals(ruleIP, targetIpStr, StringComparison.OrdinalIgnoreCase)) return true;

            if (IPAddress.TryParse(targetIpStr, out var targetAddress))
            {
                if (IPNetwork.TryParse(ruleIP, out var network))
                {
                    return network.Contains(targetAddress);
                }
            }
            return false;
        }
    }
}
