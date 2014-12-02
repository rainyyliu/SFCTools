using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Classes
{
    class ItemExchangeInMatrix
    {
        string[,] sMatrxi ={{"5","6","3","1","7","2","8","3"},
                            {"7","3","3","5","5","6","7","6"},
                            {"5","7","4","3","8","7","3","5"},
                            {"7","4","2","5","1","3","5","1"},
                            {"5","3","2","3","4","2","1","5"},
                            {"5","7","3","3","4","4","6","6"},
                            {"6","6","1","7","6","8","5","4"},
                            {"1","4","5","7","4","5","3","4"}};
        string[,] sTargetMatrix;
        public ItemExchangeInMatrix()
        {
            string sOK = "";
            bool bIsExchangeOk = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    sTargetMatrix = GetInitMatrix();
                    sTargetMatrix[i, j] = sTargetMatrix[i, j - 1];
                    sTargetMatrix[i, j - 1] = sMatrxi[i, j];
                    bIsExchangeOk = CheckAfterExchange(sTargetMatrix);
                    if (bIsExchangeOk)
                        break;
                }
                if (bIsExchangeOk)
                    break;
            }
            if (!bIsExchangeOk)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        sTargetMatrix = GetInitMatrix();
                        sTargetMatrix[i, j] = sTargetMatrix[i-1, j];
                        sTargetMatrix[i-1, j] = sMatrxi[i, j];
                        bIsExchangeOk = CheckAfterExchange(sTargetMatrix);
                        if (bIsExchangeOk)
                            break;
                    }
                    if (bIsExchangeOk)
                        break;
                }
            }
            bool xxx = bIsExchangeOk;
            string xx = sOK;
        }
        private string[,] GetInitMatrix()
        {
            string [,]sMtgt=new string[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    sMtgt[i, j] = sMatrxi[i, j];
                }
            }
            return sMtgt;
        }
        private bool CheckAfterExchange(string [,]sTarget)
        {
            bool isTridItems = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    if ((sTarget[i, j] == sTarget[i, j - 1]) && (sTarget[i, j] == sTarget[i, j + 1]))
                    {
                        isTridItems = true;
                        break;
                    }
                }
                if (isTridItems)
                    break;
            }
            if (!isTridItems)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 1; i < 7; i++)
                    {
                        if ((sTarget[i, j] == sTarget[i - 1, j]) && (sTarget[i, j] == sTarget[i + 1, j]))
                        {
                            isTridItems = true;
                            break;
                        }
                    }
                    if (isTridItems)
                        break;
                }
            }
            return isTridItems;
        }
        
    }
}
