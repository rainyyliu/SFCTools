using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    public class BlockInfo
    {
        private string blockID;
        private string blockType;
        private string blockStart;
        private string blockEnd;
        private string usedCount;
        private string printStatus;
        private string productName;
        private string currentBlock;
        private string sn;
        private string closeFlag;
        private string closeDate;
        private string lastEditBy;
        private string lastEditDT;

        public BlockInfo(string blockID, string blockType, string blockStart, string blockEnd,
            string usedCount, string printStatus, string productName, string currentBlock, string sn,
            string closeFlag, string closeDate, string lastEditBy, string lastEditDT)
        {
            this.blockID = blockID;
            this.blockType = blockType;
            this.blockStart = blockStart;
            this.blockEnd = blockEnd;
            this.usedCount = usedCount;
            this.printStatus = printStatus;
            this.productName = productName;
            this.currentBlock = currentBlock;
            this.sn = sn;
            this.closeFlag = closeFlag;
            this.closeDate = closeDate;
            this.lastEditBy = lastEditBy;
            this.lastEditDT = lastEditDT;
        }

        public string BlockID
        {
            get { return blockID; }
        }

        public string BlockType
        {
            get { return blockType; }
            set { blockType = value; }
        }

        public string BlockStart
        {
            get { return blockStart; }
        }

        public string BlockEnd
        {
            get { return blockEnd; }
        }

        public string UsedCount
        {
            get { return usedCount; }
            set { usedCount = value; }
        }

        public string PrintStatus
        {
            get { return printStatus; }
        }

        public string ProductName
        {
            get { return productName; }
        }

        public string CurrentBlock
        {
            get { return currentBlock; }
        }

        public string SN
        {
            get { return sn; }
        }

        public string CloseFlag
        {
            get { return closeFlag; }
        }

        public string CloseDate
        {
            get { return closeDate; }
        }

        public string LastEditBy
        {
            get { return lastEditBy; }
        }

        public string LastEditDT
        {
            get { return lastEditDT; }
        }
    }
}
