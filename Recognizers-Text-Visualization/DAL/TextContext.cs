using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Recognizers_Text_Visualization.Models;

namespace Recognizers_Text_Visualization.DAL
{
    public class TextContext : DbContext
    {
        public TextContext() : base("TextContext") { }

        public DbSet<Text> Texts { get; set; }
    }
}