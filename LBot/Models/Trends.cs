using System;
using System.Collections.Generic;
using System.Text;

namespace LBot.Models {
    public class Trends {
        public List<string> labels { get; set; }
        public List<List<int>> data { get; set; }
    }
}
