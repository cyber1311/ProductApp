﻿using System;
using System.Collections.Generic;

namespace ProductApp;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public virtual ProductCategory? Category { get; set; }
}
