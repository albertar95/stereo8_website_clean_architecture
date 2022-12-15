using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Setting : BaseDomainProperties
    {
        public string SettingAttribute { get; set; } = null!;
        public string SettingValue { get; set; } = null!;
    }
}
