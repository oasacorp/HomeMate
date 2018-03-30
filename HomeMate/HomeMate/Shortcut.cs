using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMate
{
    class Shortcut
    {   [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Icon { get; set; }
    }
}
