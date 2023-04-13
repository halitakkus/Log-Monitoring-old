using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Utilities.DataTransferObjects.Setting;

    public class SettingResponse
    {
        public string Key { get; set; }
        public Guid? ParentId { get; set; }
        public string Value { get; set; }
    }

