using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FideGames.Models;

namespace FideGames.Clases
{
    public class CreateInvoice
    {
        public invoice invoice { get; set; }

        public IEnumerable<product> product { get; set; }
    }
}