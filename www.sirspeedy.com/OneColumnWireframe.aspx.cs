﻿using System;
using SirSpeedy.CMS;

public partial class OneColumnWireframe : PageBuilderBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageHost1.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
    }    
}