using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToClipboard
{
    public class Options
    {
        [Option('e', "environment-variable", SetName = "env", Required = false, HelpText = "Copy target environment variable value to clipboard")]
        public bool EnvVariable { get; set; }

        [Option('c', "custom", SetName = "custom", Required = false, HelpText = "Copy target custom variable")]
        public bool Custom { get; set; }

        [Option("set", SetName = "set_custom", Required = false, HelpText = "Set custom macro clipboard")]
        public bool SetCustom { get; set; }

        [Option("delete", SetName = "delete_custom", Required = false, HelpText = "Delete custom")]
        public bool DeleteCustom  { get; set; }

        [Option('k', "key", Required = false, HelpText = "Set custom macro clipboard")]
        public string CustomKey { get; set; }

        [Option('v', "value", SetName = "set_custom", Required = false, HelpText = "Set custom macro clipboard")]
        public string CustomValue { get; set; }

        [Option('p', "print", SetName = "print", Required = false, HelpText = "Print current clipboard contents")]
        public bool Print { get; set; }

        [Option('i', "input", Required = false, HelpText = "input")]
        public string Input { get; set; }
    }
}
