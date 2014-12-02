using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace SFC_Tools.Model
{
    public class MdRepairStationsInfo
    {
        private string sToGroupName;
        private string sFromGroupName;
        private Point pointToGroup;
        private Point pointFromGroup;
        private bool bisExits;

        public string FROM_STATION_GROUP
        {
            set { this.sFromGroupName = value; }
            get { return this.sFromGroupName; }
        }

        public string TO_STATION_GROUP
        {
            set { this.sToGroupName = value; }
            get { return this.sToGroupName; }
        }

        public Point TO_STATION_POSITION
        {
            set { this.pointToGroup = value; }
            get { return this.pointToGroup; }
        }

        public Point FROM_STATION_POSITION
        {
            set { this.pointFromGroup = value; }
            get { return this.pointFromGroup; }
        }

        public bool ISEXISTS
        {
            set { this.bisExits = value; }
            get { return this.bisExits; }
        }
    }
}
