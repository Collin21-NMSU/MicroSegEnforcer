using System;
using System.Collections.Generic;
using System.Text;

namespace MicroSegEnforcer
{
    public class NetworkRule
    {
        public string RuleName { get; set; }
        public string SourceIp { get; set; }
        public string DestinationIp { get; set; }
        public int Port { get; set; }
        public string Action { get; set; } // "allow" or "deny"
    }
    public class FirewallPolicy
    {
        public string PolicyName { get; set; }
        public List<NetworkRule> Rules { get; set; } = new List<NetworkRule>();

    }
}
