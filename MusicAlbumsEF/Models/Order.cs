﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AlbumId { get; set; }
    }
}
