using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;

namespace WebAPI.DataProvider.Models
{
    public class LayerInfo
    {
        public string LayerID { get; set; }
        public string LayerTitle { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsCreate { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsEdit { get; set; }
        public string OutFields { get; set; }
        public string QueryFields { get; set; }
        public string UpdateFields { get; set; }
        public string Definition { get; set; }
        public string Url { get; set; }
        public GroupLayer GroupLayer { get; set; }
    }
    public class GroupLayer : SYS_GroupLayer
    {
    }
}
